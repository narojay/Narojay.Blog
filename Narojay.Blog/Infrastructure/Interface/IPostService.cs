using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IPostService
    {

        Task<bool> AddPostAsync(Post post);

        Task<Post> GetPostAsync(int id);
    }
}
