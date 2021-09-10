using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
