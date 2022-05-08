using MediatR;

namespace Narojay.Blog.Models.Dto
{
    public class CreateOrderCommand : IRequest<string>
    {
        public string OrderName { get; set; }
    }
}
