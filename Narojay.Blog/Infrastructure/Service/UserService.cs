using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Narojay.Tools.Core.Security;

namespace Narojay.Blog.Infrastructure.Service
{
    public class UserService : IUserService
    {
        public DataContext Context { get; set; }

        public async Task<List<User>> GetAllUserAsync() =>
            await RedisHelper.CacheShellAsync(RedisPrefix.GetAllUser, 60 * 60 * 24,
                () => Context.Users.ToListAsync());

        public async Task<bool> AddUserAsync(User user)
        {
            user.Password= Encrypt.Md5Encrypt(user.Password);
            await Context.AddAsync(user);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ResetPassword(int id, string password)
        {
            var md5Password = Encrypt.Md5Encrypt(password);
            var user =await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.Password = md5Password;
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
