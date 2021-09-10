using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class PostService : IPostService
    {
        public DataContext Context { get; set; }

        public async Task<bool> AddPostAsync(Post post)
        {
            await Context.Posts.AddAsync(post);

            return await Context.SaveChangesAsync() > 0;
        }

        public Task<List<User>> GetUserAsync()
        {
            var blogUsers = Context.Users.ToListAsync();
            return blogUsers;
        }
    }
}
