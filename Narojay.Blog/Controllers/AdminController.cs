using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Controllers
{
    [Route("admin")]
    public class AdminController : BaseController
    {
        public IPostService PostService { get; set; }


        [HttpPost("publish_post")]
        public Task<bool> AddPost(Post post)
        {
            return PostService.AddPostAsync(post);
        }


        [HttpPost("posts")]
        public Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request)
        {
            return PostService.GetPostAdminAsync(request);
        }


        [HttpGet("statistic")]
        public Task<List<StatisticDto>> GetStatisticDtoAsync()
        {
            return PostService.GetStatisticDtoAsync();
        }

        //1231
        [HttpGet("labels")]
        public Task<List<string>> GetLabelsAsync()
        {
            return PostService.GetLabelsAsync();
        }

        [HttpPost("post/delete")]
        public Task<bool> DeleteArticleById(int id)
        {
            return PostService.DeleteArticleById(id);
        }
    }
}