using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    [Route("Post")]
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;
        private readonly DataContext _context;

        public IPostService PostService { get; set; }

        public PostController(ILogger<PostController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 文章id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public Task<Post> GetPostById(int id) => PostService.GetPostByIdAsync(id);


        [HttpPost("add")]
        public Task<bool> AddPost(Post post) => PostService.AddPostAsync(post);
    }
}
