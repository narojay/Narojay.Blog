using MediatR;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{
    [Route("notice")]
    public class NoticeController : BaseController
    {
        private readonly IAdminNoticeService _adminNoticeService;
        private readonly IMediator _mediator;

        public NoticeController(IAdminNoticeService adminNoticeService, IMediator mediator)
        {
            _adminNoticeService = adminNoticeService;
            _mediator = mediator;
        }


        [HttpGet("message")]
        public Task<string> GetAdminNotice()
        {
            return _adminNoticeService.GetAdminNoticeAsync();
        }

        [HttpPost("msessage/edit")]
        public Task<bool> EditAdminNoticeAsync(string content)
        {
            return _adminNoticeService.EditAdminNoticeAsync(content);
        }


        [HttpPost("test")]
        public async Task<string> CreateOrder([FromBody] CreateOrderCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}