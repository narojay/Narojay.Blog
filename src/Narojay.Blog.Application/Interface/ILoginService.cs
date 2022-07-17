using System.Threading.Tasks;

namespace Narojay.Blog.Application.Interface;

public interface ILoginService
{
    Task<string> LoginAsync(string username, string password);
}