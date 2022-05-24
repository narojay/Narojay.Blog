using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public BlogContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public async Task<bool> AddPostAsync(Post post)
        {
            await Context.Posts.AddAsync(post);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            return await RedisHelper.CacheShellAsync("PostContent", id.ToString(), 0, async () =>
             {
                 var post = Context.Posts.FirstOrDefault(x => x.Id == id);
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
                         var post = Context.Posts.FirstOrDefault(x => x.Id == id);
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
                    var result = Context.Posts.AsNoTracking().GroupBy(x => x.Label).Select(x => new
                    {
                        x.Key,
                        Count = x.Count()
                    }).ToDictionary(x => x.Key, x => x.Count);
                    return result;
                });
        }

        public async Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request)
        {
            var query = Context.Posts.WhereIf(string.IsNullOrEmpty(request.Title), x => x.Title.Contains(request.Title))
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
            var labelNum = await Context.Posts.GroupBy(x => x.Label).Select(x => x.Key).CountAsync();
            var postNum = await Context.Posts.CountAsync();

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
                    var result = await Context.Posts.Select(x => x.Label).Distinct().ToListAsync();
                    return result;
                });
        }

        public async Task<bool> DeleteArticleById(int id)
        {
            var post = await Context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null) return false;

            Context.Posts.Remove(post);
            return await Context.SaveChangesAsync() > 0;
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
            var post = await Context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (status == LikeOrUnlike.Like)
            {
                post.LikeCount++;
            }
            else if (status == LikeOrUnlike.Unlike)
            {
                post.UnlikeCount++;
            }
            if (await Context.SaveChangesAsync() > 0)
            {
                await RedisHelper.HDelAsync("PostContent", post.Id.ToString());
                return true;

            }
            return false;
        }
    }
}