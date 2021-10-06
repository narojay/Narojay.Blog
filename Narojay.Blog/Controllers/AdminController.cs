using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
    [Route("admin")]
    public class AdminController : BaseController
    {

        public IPostService PostService { get; set; }



        [Authorize]
        [HttpPost("publish_post")]
        public Task<bool> AddPost(Post post) => PostService.AddPostAsync(post);

    }
}
