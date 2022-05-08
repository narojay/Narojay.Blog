using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IAdminNoticeService
    {
        Task<string> GetAdminNoticeAsync();
        Task<bool> EditAdminNoticeAsync(string content);
        Task TestTranscation();
    }
}