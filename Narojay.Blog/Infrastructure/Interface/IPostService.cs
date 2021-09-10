using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IPostService
    {

        public Task<bool> AddPostAsync(Post post);

        public Task<List<User>> GetUserAsync();
    }
}
