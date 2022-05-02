using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    ///     文章
    /// </summary>
    [Route("post")]
    public class PostController : BaseController
    {
        private readonly DataContext _context;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IPostService PostService { get; set; }

        /// <summary>
        ///     文章id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public PostDto GetPostById(int id)
        {
            return PostService.GetPostByIdAsync(id);
        }


        /// <summary>
        ///     获取文章分页
        /// </summary>
        /// <param name="pageInputBaseDto"></param>
        /// <returns></returns>
        [HttpPost("posts")]
        public Task<PageOutputDto<PostDto>> GetPostList(PageInputBaseDto pageInputBaseDto)
        {
            return PostService.GetPostListAsync(pageInputBaseDto);
        }


        /// <summary>
        ///     获取label统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("label_statistics")]
        public Dictionary<string, int> GetLabelStatistics()
        {
            return PostService.GetLabelStatistics();
        }
    }
}