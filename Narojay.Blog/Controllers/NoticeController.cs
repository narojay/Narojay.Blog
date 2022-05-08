using MediatR;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Narojay.Blog.Configs;

namespace Narojay.Blog.Controllers
{
    [Route("notice")]
    public class NoticeController : BaseController
    {
        private readonly IAdminNoticeService _adminNoticeService;
        private readonly IOptions<RabbitMqConfig> _options;
        private readonly IMediator _mediator;

        public NoticeController(IAdminNoticeService adminNoticeService, IOptions<RabbitMqConfig> options,IMediator mediator)
        {
            _adminNoticeService = adminNoticeService;
            _options = options;
            _mediator = mediator;
        }


        [HttpGet("message")]
        public Task<string> GetAdminNotice()
        {
            Console.WriteLine("testestset");
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


        [HttpPost("test1")]
        public async Task CreateOrder1()
        {
            await _adminNoticeService.TestTranscation();
        }
    }
}