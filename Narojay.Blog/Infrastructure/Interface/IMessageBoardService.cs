using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IMessageBoardService
    {
        Task<LeaveMessage> AddLeaveMessageAsync(LeaveMessageDto message);
        Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessagePageAsync(PageInputDto message);
        Task<bool> RemoveLeaveMessageAsync(int id);
        Task<bool> BatchLeaveMessageAsync(int num);
        Task<bool> BatchUpdateLeaveMessageAsync(int num);
    }
}
