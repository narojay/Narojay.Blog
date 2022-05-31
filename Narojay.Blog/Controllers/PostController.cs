using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Tools.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    ///     文章
    /// </summary>
    [Route("post")]
    public class PostController : BaseController
    {
        private readonly BlogContext _context;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger, BlogContext context)
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
        public async Task<PostDto> GetPostById(int id)
        {
            return await PostService.GetPostByIdAsync(id);
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

        [HttpPost("like_unlike_count")]
        public Task<bool> AddLikeOrUnlikeCountAsync(int id, LikeOrUnlike status) => PostService.AddLikeOrUnlikeCountAsync(id, status);

        [HttpPost("tag")]
        public Task<bool> AddTagAsync(TagDto tag) => PostService.AddTagAsync(tag);


        [HttpGet("tags")]
        public Task<IList<IdAndNameDto>> GetTagsAsync() => PostService.GetTagsAsync();

        [HttpPut("post_tag")]
        public Task<bool> AddPostTagAsync(PostTagDto postTagDto) => PostService.AddPostTagAsync(postTagDto);


    }
}