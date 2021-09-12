﻿using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Tools.Core.Dto;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
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
    }
}
