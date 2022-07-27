using EventBus.Events;

namespace Narojay.Blog.Application.Events;

public class CreateOrderEvent : IntegrationEvent
{
    public int Num { get; set; }

    public string Name { get; set; }
}