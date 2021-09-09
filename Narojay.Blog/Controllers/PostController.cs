using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{
    [Route("Post")]
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;
        private readonly DataContext _context;

        public ITestService TestService { get; set; }

        public PostController(ILogger<PostController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<List<User>> GetBlog()
        {
            var blogUsers = await _context.Users.ToListAsync();
            return blogUsers;
        }

        [HttpPost("add")]
        public async Task<bool> AddPost()
        {
            await _context.AddAsync(new User
            {
                Age = 20+ new Random().Next(40),
                Email = new Random().Next(10000000) + "@126.com",
                UserName = "narojay" + new Random().Next(1000000),
                NickName = "narojay" + new Random().Next(1000000),
                Remarks = "too young too simple"
            });
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
