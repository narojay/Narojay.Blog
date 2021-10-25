using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Narojay.Blog.Configs;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {
        private readonly IEnumerable<IConfigureOptions<Test>> _test;


        public UserController(IEnumerable<IConfigureOptions<Test>> test)
        {
            _test = test;
        }
        public  IUserService UserService { get; set; }

        [HttpGet("users")]
        public Task<List<User>> GetAllUser() => UserService.GetAllUserAsync();

        [Authorize]
        [HttpPost("add")]
        public Task<bool> AddUser(User user) => UserService.AddUserAsync(user);


        [Authorize]
        [HttpPost("reset_password")]
        public Task<bool> ResetPassword(int id,string password) => UserService.ResetPassword(id,password);



        [HttpPost("reset_password1")]
        public Task<bool> ResetPassword()
        {
            var options = _test;
            return  Task.FromResult(true);
        }
    }
}
