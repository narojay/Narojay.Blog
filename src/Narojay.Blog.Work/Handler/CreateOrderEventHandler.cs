using System;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Application.Events;
using Narojay.Blog.Application.Interface;

namespace Narojay.Blog.Work.Handler;

public class CreateOrderEventHandler : IIntegrationEventHandler<CreateOrderEvent>
{
    private readonly ILogger<CreateOrderEventHandler> _logger;
    private readonly IPostService _postService;

    public CreateOrderEventHandler(ILogger<CreateOrderEventHandler> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }


    public async Task Handle(CreateOrderEvent @event)
    {
        //   var postByIdAsync = await _postService.GetPostByIdAsync(@event.Num);
        // var a =  JsonSerializer.Serialize(postByIdAsync,new JsonSerializerOptions
        //   {
        //       
        //       Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        //   });
        //   _logger.LogInformation(a);
        Console.WriteLine(_postService.GetHashCode());
    }
}