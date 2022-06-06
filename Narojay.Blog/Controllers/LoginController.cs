using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Application.Service;

namespace Narojay.Blog.Controllers;

[Route("login")]
public class LoginController : BaseController
{
    public LoginService LoginService { get; set; }

    [HttpPost("login")]
    public Task<string> LoginAsync(string username, string password)
    {
        return LoginService.LoginAsync(username, password);
    }
}