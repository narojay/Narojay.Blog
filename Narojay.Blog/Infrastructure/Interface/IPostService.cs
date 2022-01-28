using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IPostService
    {

        Task<bool> AddPostAsync(Post post);

        PostDto GetPostByIdAsync(int id);
        Task<PageOutputDto<PostDto>> GetPostListAsync(PageInputBaseDto pageInputBaseDto);
        Dictionary<string, int> GetLabelStatistics();
        Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request);
        Task<List<StatisticDto>> GetStatisticDtoAsync();
        Task<List<string>> GetLabelsAsync();
        Task<bool> DeleteArticleById(int id);
    }
}
