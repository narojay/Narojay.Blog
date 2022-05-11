using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using Narojay.Tools.Core.Security;

namespace Narojay.Blog.Infrastructure.Service
{
    public class UserService : IUserService
    {
        public BlogContext Context { get; set; }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await RedisHelper.CacheShellAsync(RedisPrefix.GetAllUser, 60 * 60 * 24,
                () => Context.Users.ToListAsync());
        }

        public async Task<bool> AddUserAsync(User user)
        {
            user.Password = Encrypt.Md5Encrypt(user.Password);
            await Context.AddAsync(user);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ResetPassword(int id, string password)
        {
            var md5Password = Encrypt.Md5Encrypt(password);
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.Password = md5Password;
            return await Context.SaveChangesAsync() > 0;
        }
    }
}