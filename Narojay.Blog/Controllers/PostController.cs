using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Narojay.Blog.Infrastructure.Service;
using Narojay.Blog.Models.Dto;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    [Route("post")]
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
     
        public PostDto GetPostById(int id) => PostService.GetPostByIdAsync(id);




        /// <summary>
        /// 获取文章分页
        /// </summary>
        /// <param name="pageInputBaseDto"></param>
        /// <returns></returns>
        [HttpPost("posts")]
        public Task<PageOutputDto<PostDto>> GetPostList(PageInputBaseDto pageInputBaseDto) => PostService.GetPostListAsync(pageInputBaseDto);


        /// <summary>
        /// 获取label统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("label_statistics")]
        public Dictionary<string,int> GetLabelStatistics() => PostService.GetLabelStatistics();

    }
}
