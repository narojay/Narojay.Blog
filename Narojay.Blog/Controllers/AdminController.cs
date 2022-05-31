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
        public Task<bool> AddPost(PostDto postDto)
        {
            return PostService.AddPostAsync(postDto);
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
        public Task<bool> DeleteArticleById(int id) => PostService.DeleteArticleById(id);

        [HttpGet("aboutme")]
        public Task<string> GetAboutMeContentAsync() => PostService.GetAboutMeContentAsync();

        [HttpPost("aboutme/modify")]
        public Task<bool> ModifiyAboutMeContentAsync(AboutMeDto aboutMeDto) => PostService.ModifiyAboutMeContentAsync(aboutMeDto);
    }
}