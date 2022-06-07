using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity.Test;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Controllers;

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
    public Task<bool> DeleteArticleById(int id)
    {
        return PostService.DeleteArticleById(id);
    }

    [HttpGet("aboutme")]
    public Task<string> GetAboutMeContentAsync()
    {
        return PostService.GetAboutMeContentAsync();
    }

    [HttpPost("aboutme/modify")]
    public Task<bool> ModifiyAboutMeContentAsync(AboutMeDto aboutMeDto)
    {
        return PostService.ModifiyAboutMeContentAsync(aboutMeDto);
    }
}