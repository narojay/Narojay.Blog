using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Infrastruct.DataBase;
using Narojay.Blog.Infrastruct.Jwt;
using Narojay.Tools.Core.Security;

namespace Narojay.Blog.Application.Service;

public class LoginService : ILoginService
{
    public IJwtService JwtService { get; set; }
    public BlogContext BlogContext { get; set; }

    public async Task<string> LoginAsync(string username, string password)
    {
        var dataPassword = await BlogContext.Users.AsNoTracking().Where(x => x.UserName == username)
            .Select(x => x.Password)
            .FirstOrDefaultAsync();
        var a = Encrypt.Md5Encrypt(password);
        var status = dataPassword == Encrypt.Md5Encrypt(password);
        return status ? JwtService.CreateJwtToken(username) : "";
    }
}