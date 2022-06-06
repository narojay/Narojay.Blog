using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Infrastruct.Common;

namespace Narojay.Blog.Controllers;

[Route("notice")]
public class NoticeController : BaseController
{
    private readonly IAdminNoticeService _adminNoticeService;
    private readonly IMediator _mediator;
    private readonly IOptions<RabbitMqConfig> _options;

    public NoticeController(IAdminNoticeService adminNoticeService, IOptions<RabbitMqConfig> options,
        IMediator mediator)
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