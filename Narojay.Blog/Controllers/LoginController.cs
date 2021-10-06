using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Service;
using Narojay.Blog.Models.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Narojay.Blog.Controllers
{
    [Route("login")]
    public class LoginController : BaseController
    {
        public LoginService LoginService { get; set; }
        [HttpPost("login")]
        public Task<string> LoginAsync(string username,string password) => LoginService.LoginAsync( username,password);

    }
}
