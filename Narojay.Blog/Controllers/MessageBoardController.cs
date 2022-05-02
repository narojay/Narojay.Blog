using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    ///     留言板
    /// </summary>
    [Route("messageboard")]
    public class MessageBoardController : BaseController
    {
        public IMessageBoardService MessageBoardService { get; set; }

        [HttpPost("add")]
        public Task<LeaveMessage> AddLeaveMessage(LeaveMessageDto message)
        {
            return MessageBoardService.AddLeaveMessageAsync(message);
        }


        [HttpPost("pages")]
        public Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessageAsync(PageInputDto message)
        {
            return MessageBoardService.GetLeaveMessagePageAsync(message);
        }

        [HttpPost("delete/{id}")]
        public Task<bool> RemoveLeaveMessageAsync(int id)
        {
            return MessageBoardService.RemoveLeaveMessageAsync(id);
        }


        [HttpPost("batch/add")]
        public Task<bool> BatchLeaveMessageAsync(int num)
        {
            return MessageBoardService.BatchLeaveMessageAsync(num);
        }


        [HttpPost("batch/update")]
        public Task<bool> BatchUpdateLeaveMessageAsync(int num)
        {
            return MessageBoardService.BatchUpdateLeaveMessageAsync(num);
        }
    }
}