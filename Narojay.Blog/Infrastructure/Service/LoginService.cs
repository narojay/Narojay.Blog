using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Tools.Core.Security;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class LoginService : ILoginService
    {
        public IJwtService JwtService { get; set; }
        public DataContext DataContext { get; set; }
        public async Task<string> LoginAsync(string username, string password)
        {
            var dataPassword = await DataContext.Users.AsNoTracking().Where(x => x.UserName == username).Select(x => x.Password)
                .FirstOrDefaultAsync();
            var a = Encrypt.Md5Encrypt(password);
            var status = dataPassword == Encrypt.Md5Encrypt(password);
            return status ? JwtService.CreateJwtToken(username) : "";
        }
    }
}
