using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class UserService : IUserService
    {
        public DataContext Context { get; set; }

        public async Task<List<User>> GetAllUserAsync()
        {
            var users = await RedisHelper.GetAsync<List<User>>(RedisPrefix.GetAllUser);
            if (users != null)
            {
                return users;
            }
            await RedisHelper.SetAsync(RedisPrefix.GetAllUser, await Context.Users.ToListAsync(), TimeSpan.FromDays(1));
            return await RedisHelper.GetAsync<List<User>>(RedisPrefix.GetAllUser);
        }
    }
}
