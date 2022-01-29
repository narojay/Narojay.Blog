using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Tools.Core.Dto;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    /// 留言板
    /// </summary>
    [Route("messageboard")]
    public class MessageBoardController : BaseController
    {
        public IMessageBoardService MessageBoardService { get; set; }   

        [HttpPost("add")]
        public Task<LeaveMessage> AddLeaveMessage(LeaveMessageDto message) =>
            MessageBoardService.AddLeaveMessageAsync(message);


        [HttpPost("pages")]
        public Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessageAsync(PageInputDto message) =>
            MessageBoardService.GetLeaveMessagePageAsync(message);

        [HttpPost("delete/{id}")]
        public Task<bool> RemoveLeaveMessageAsync(int id) =>
            MessageBoardService.RemoveLeaveMessageAsync(id);



        [HttpPost("batch/add")]
        public Task<bool> BatchLeaveMessageAsync(int num) =>
            MessageBoardService.BatchLeaveMessageAsync(num);


        [HttpPost("batch/update")]
        public Task<bool> BatchUpdateLeaveMessageAsync(int num) =>
            MessageBoardService.BatchUpdateLeaveMessageAsync(num);
    }
}
