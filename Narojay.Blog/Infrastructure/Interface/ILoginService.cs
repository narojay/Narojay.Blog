using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string username, string password);
    }
}