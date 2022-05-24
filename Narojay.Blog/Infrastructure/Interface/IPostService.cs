﻿using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IPostService
    {
        Task<bool> AddPostAsync(Post post);
        Task<PostDto> GetPostByIdAsync(int id);
        Task<PageOutputDto<PostDto>> GetPostListAsync(PageInputBaseDto pageInputBaseDto);
        Dictionary<string, int> GetLabelStatistics();
        Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request);
        Task<List<StatisticDto>> GetStatisticDtoAsync();
        Task<List<string>> GetLabelsAsync();
        Task<bool> DeleteArticleById(int id);
        Task<string> GetAboutMeContentAsync();
        Task<bool> ModifiyAboutMeContentAsync(AboutMeDto aboutMeDto);
        Task<bool> AddLikeOrUnlikeCountAsync(int id,LikeOrUnlike status);
    }
}