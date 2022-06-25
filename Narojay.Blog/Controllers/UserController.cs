using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Entity;

namespace Narojay.Blog.Controllers;

[Route("User")]
public class UserController : BaseController
{
    public IUserService UserService { get; set; }

    [HttpGet("users")]
    public Task<List<User>> GetAllUser()
    {
        return UserService.GetAllUserAsync();
    }

    [HttpPost("add")]
    public Task<bool> AddUser(User user)
    {
        return UserService.AddUserAsync(user);
    }


    [Authorize]
    [HttpPost("reset_password")]
    public Task<bool> ResetPassword(int id, string password)
    {
        return UserService.ResetPassword(id, password);
    }


    [HttpPost("reset_password1")]
    public Task<bool> ResetPassword()
    {
        return Task.FromResult(true);
    }

    [HttpGet("ip")]
    public Task<string> GetUserIp()
    {
        var ip = HttpContext.Connection.RemoteIpAddress.ToString();
        return Task.FromResult(ip);
    }
}