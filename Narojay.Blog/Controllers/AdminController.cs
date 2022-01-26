﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public Task<bool> AddPost(Post post) => PostService.AddPostAsync(post);



        [HttpPost("posts")]
        public Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request) => PostService.GetPostAdminAsync(request);


        [HttpGet("statistic")]
        public Task<List<StatisticDto>> GetStatisticDtoAsync() => PostService.GetStatisticDtoAsync();

        [HttpGet("labels")]
        public Task<List<string>> GetLabelsAsync() => PostService.GetLabelsAsync();
    }
}
