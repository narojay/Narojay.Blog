using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Aop;
using Narojay.Blog.Extensions;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using Narojay.Tools.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class PostService : IPostService
    {
        private readonly ILogger<PostService> _logger;

        public PostService(ILogger<PostService> logger)
        {
            _logger = logger;
        }
        public BlogContext BlogContext { get; set; }
        public IMapper Mapper { get; set; }

        public async Task<bool> AddPostAsync(PostDto postDto)
        {
            var post = new Post(postDto.Title, postDto.Content, postDto.Author, postDto.IsTop,postDto.UserId);

            foreach (var tagName in postDto.PostTagDto.TagNames)
            {
                post.AddPostTags(0, tagName);
            }

            foreach (var tagId in postDto.PostTagDto.TagIds)
            { 
                post.AddPostTags(tagId);
            }

            await BlogContext.Posts.AddAsync(post);

            return await BlogContext.SaveChangesAsync() > 0;
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            return await RedisHelper.CacheShellAsync("PostContent", id.ToString(), 0, async () =>
             {
                 var post = BlogContext.Posts.FirstOrDefault(x => x.Id == id);
                 var postDto = Mapper.Map<PostDto>(post);
                 return postDto;
             });

        }

        public async Task<PageOutputDto<PostDto>> GetPostListAsync(PageInputBaseDto pageInputBaseDto)
        {

            var a = await RedisHelper.ZRevRangeAsync("PostSortByTime", (pageInputBaseDto.PageIndex - 1) * pageInputBaseDto.PageSize, pageInputBaseDto.PageIndex * pageInputBaseDto.PageSize - 1);
            var postDtos = new List<PostDto>();

            foreach (var s in a)
            {
                var postDto = await RedisHelper.CacheShellAsync("PostContent", s, 0, async () =>
                     {
                         var id = Convert.ToInt32(s);
                         var post = BlogContext.Posts.FirstOrDefault(x => x.Id == id);
                         var postDto = Mapper.Map<PostDto>(post);
                         return postDto;
                     });

                postDtos.Add(postDto);
            }

            return new PageOutputDto<PostDto>
            {
                TotalCount = (int)await RedisHelper.ZCardAsync("PostSortByTime"),
                Data = postDtos
            };
        }

        public Dictionary<string, int> GetLabelStatistics()
        {
            return RedisHelper.CacheShell(RedisPrefix.GetTagStatistics, 180,
                () =>
                {
                    var result = BlogContext.PostTags.AsNoTracking().Include(x => x.Tag).GroupBy(x => x.Tag.Name).Select(x => new
                    {
                        x.Key,
                        Count = x.Count()
                    }).ToDictionary(x => x.Key, x => x.Count);
                    return result;
                });
        }

        public async Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request)
        {
            var query = BlogContext.Posts.WhereIf(string.IsNullOrEmpty(request.Title), x => x.Title.Contains(request.Title))
                .WhereIf(string.IsNullOrEmpty(request.Label), x => x.Label.Contains(request.Label))
                .OrderByDescending(x => x.CreationTime);
            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x =>
                new PostAdminDto
                {
                    Id = x.Id,
                    CreationTime = x.CreationTime,
                    Label = x.Label,
                    Title = x.Title
                }).ToListAsync();
            var total = await query.CountAsync();
            return new PageOutputDto<PostAdminDto>
            {
                Data = result,
                TotalCount = total
            };
        }

        public async Task<List<StatisticDto>> GetStatisticDtoAsync()
        {
            var labelNum = await BlogContext.Posts.GroupBy(x => x.Label).Select(x => x.Key).CountAsync();
            var postNum = await BlogContext.Posts.CountAsync();

            var result = new List<StatisticDto>
            {
                new()
                {
                    Name = "标签",
                    Num = labelNum
                },
                new()
                {
                    Name = "文章",
                    Num = postNum
                }
            };
            return result;
        }

        public async Task<List<string>> GetLabelsAsync()
        {
            return await RedisHelper.CacheShellAsync(RedisPrefix.GetLabelSelect, 180,
                async () =>
                {
                    var result = await BlogContext.Posts.Select(x => x.Label).Distinct().ToListAsync();
                    return result;
                });
        }

        public async Task<bool> DeleteArticleById(int id)
        {
            var post = await BlogContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null) return false;

            BlogContext.Posts.Remove(post);
            return await BlogContext.SaveChangesAsync() > 0;
        }

        public async Task<string> GetAboutMeContentAsync()
        {
            var result = await RedisHelper.GetAsync<string>(RedisPrefix.GetAboutMeContentAsync);
            return result ?? string.Empty;
        }

        public async Task<bool> ModifiyAboutMeContentAsync(AboutMeDto aboutMeDto)
        {
            var result = await RedisHelper.SetAsync(RedisPrefix.GetAboutMeContentAsync, aboutMeDto.Content);
            return result;
        }

        public async Task<bool> AddLikeOrUnlikeCountAsync(int id, LikeOrUnlike status)
        {
            var post = await BlogContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (status == LikeOrUnlike.Like)
            {
                post.LikeCount++;
            }
            else if (status == LikeOrUnlike.Unlike)
            {
                post.UnlikeCount++;
            }
            if (await BlogContext.SaveChangesAsync() > 0)
            {
                await RedisHelper.HDelAsync("PostContent", post.Id.ToString());
                return true;

            }
            return false;
        }

        public async Task<bool> AddTagAsync(int id, string name)
        {
            await BlogContext.Tags.AddAsync(new Tag(name));
            return await BlogContext.SaveChangesAsync() > 0;
        }

        public async Task<IList<IdAndNameDto>> GetTagsAsync()
        {
            return await RedisHelper.CacheShellAsync("tags", 0, async () => await BlogContext.Tags.Select(x => new IdAndNameDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync());

        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var tag = await BlogContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag == null)
            {
                throw new StringResponseException("标签不存在或者已删除");
            }

            BlogContext.Tags.Remove(tag);

            return await BlogContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddTagAsync(TagDto tag)
        {
            if (await BlogContext.Tags.AnyAsync(x => x.Name == tag.Name))
            {
                throw new StringResponseException("标签名已存在");
            }
            var newTag = new Tag(tag.Name);

            await BlogContext.Tags.AddAsync(newTag);
            return await BlogContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddPostTagAsync(PostTagDto postTagDto)
        {

            if (!await BlogContext.Posts.AnyAsync(x => x.Id == postTagDto.PostId))
            {
                throw new StringResponseException("文章状态异常或不存在");
            }
            var tags = await BlogContext.Tags.Select(x => x.Name).ToListAsync();

            var newTags = postTagDto.TagNames
                .Where(x => !tags.Contains(x)).
                Select(x => new PostTags(postTagDto.PostId, 0, x));

            var newPostTags = postTagDto.TagIds.Select(x => new PostTags(postTagDto.PostId, x));

            var addPostTags = newTags.Union(newPostTags);
            await BlogContext.PostTags.AddRangeAsync(addPostTags);

            var result = await BlogContext.SaveChangesAsync() > 0;
            if (result)
            {
                await RedisHelper.DelAsync("tags");
            }

            return result;
        }
    }
}