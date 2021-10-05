﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using Narojay.Tools.Core.Dto;
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

        public  PostDto GetPostByIdAsync(int id) =>
             RedisHelper.CacheShell(RedisPrefix.GetPost + id, 18000,
                 () =>
                {
                    var result =  Context.Posts.FirstOrDefault(x => x.Id == id);
                    var model = Mapper.Map<PostDto>(result);
                    return model;
                });

        public async Task<PageOutputDto<PostDto>> GetPostListAsync(PageInputBaseDto pageInputBaseDto)
        {
            var result = await Context.Posts.Skip((pageInputBaseDto.PageIndex - 1) * pageInputBaseDto.PageSize).Take(pageInputBaseDto.PageSize).OrderByDescending(x => x.CreationTime).Select(x => new PostDto
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





    }
}
