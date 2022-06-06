using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Application.Interface;

public interface IMessageBoardService
{
    Task<LeaveMessage> AddLeaveMessageAsync(LeaveMessageDto message);
    Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessagePageAsync(PageInputDto message);
    Task<bool> RemoveLeaveMessageAsync(int id);
    Task<bool> BatchLeaveMessageAsync(int num);
    Task<bool> BatchUpdateLeaveMessageAsync(int num);
}