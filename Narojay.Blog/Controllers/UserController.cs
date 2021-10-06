using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {

        public  IUserService UserService { get; set; }

        [HttpGet("users")]
        public Task<List<User>> GetAllUser() => UserService.GetAllUserAsync();

        [Authorize]
        [HttpPost("add")]
        public Task<bool> AddUser(User user) => UserService.AddUserAsync(user);


        [Authorize]
        [HttpPost("reset_password")]
        public Task<bool> ResetPassword(int id,string password) => UserService.ResetPassword(id,password);
    }
}
