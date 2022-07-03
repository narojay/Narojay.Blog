using System.Text.Encodings.Web;
using System.Text.Json;
using EventBus.Abstractions;
using Narojay.Blog.Application.Events;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Infrastruct.DataBase;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Narojay.Blog.Work.Handler;

public class CreateOrderEventHandler : IIntegrationEventHandler<CreateOrderEvent>
{
    private readonly ILogger<CreateOrderEventHandler> _logger;
    private readonly IPostService _postService;

    public CreateOrderEventHandler(ILogger<CreateOrderEventHandler> logger,IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

  
    public async Task Handle(CreateOrderEvent @event)
    {
        var postByIdAsync = await _postService.GetPostByIdAsync(@event.Num);
      var a =  JsonSerializer.Serialize(postByIdAsync,new JsonSerializerOptions
        {
            
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });
        _logger.LogInformation(a);
        
    }
}