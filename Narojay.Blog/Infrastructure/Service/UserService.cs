using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class UserService : IUserService
    {
        public DataContext Context { get; set; }

        public async Task<List<User>> GetAllUserAsync()
        {
          return  await RedisHelper.CacheShellAsync(RedisPrefix.GetAllUser, 60 * 60 * 24,
                 () => Context.Users.ToListAsync());
        }
    }
}
