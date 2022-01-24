using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Extensions;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using Narojay.Tools.Core.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class PostService : IPostService
    {
        public DataContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public async Task<bool> AddPostAsync(Post post)
        {
            await Context.Posts.AddAsync(post);
            return await Context.SaveChangesAsync() > 0;
        }

        public PostDto GetPostByIdAsync(int id) =>
             RedisHelper.CacheShell(RedisPrefix.GetPost + id, 18000,
                 () =>
                {
                    var result = Context.Posts.FirstOrDefault(x => x.Id == id);
                    var model = Mapper.Map<PostDto>(result);
                    return model;
                });

        public async Task<PageOutputDto<PostDto>> GetPostListAsync(PageInputBaseDto pageInputBaseDto)
        {
            var result = await Context.Posts.OrderByDescending(x => x.CreationTime).Skip((pageInputBaseDto.PageIndex - 1) * pageInputBaseDto.PageSize).Take(pageInputBaseDto.PageSize).Select(x => new PostDto
            {
                Id = x.Id,
                Author = x.Author,
                Title = x.Title,
                CreationTime = x.CreationTime,
                Label = x.Label
            }).ToListAsync();

            return new PageOutputDto<PostDto>
            {
                TotalCount = await Context.Posts.CountAsync(),
                Data = result
            };
        }

        public Dictionary<string, int> GetLabelStatistics() =>
            RedisHelper.CacheShell(RedisPrefix.GetTagStatistics, 18000,
                () =>
                {
                    var result = Context.Posts.AsNoTracking().GroupBy(x => x.Label).Select(x => new
                    {
                        x.Key,
                        Count = x.Count()
                    }).ToDictionary(x => x.Key, x => x.Count);
                    return result;
                });

        public async Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request)
        {
            var query = Context.Posts.WhereIf(string.IsNullOrEmpty(request.Title), x => x.Title.Contains(request.Title))
                                                          .WhereIf(string.IsNullOrEmpty(request.Label), x => x.Label.Contains(request.Label))
                                                          .OrderByDescending(x => x.CreationTime);
            var result = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new PostAdminDto
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
                new StatisticDto
                {
                    Name = "label",
                    Num = labelNum,

                },
                new StatisticDto
                {
                    Name = "post",
                    Num = postNum,

                },
            };
            return result;
        }
    }
}
