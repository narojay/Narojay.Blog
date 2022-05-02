using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Narojay.Blog.Aop;

namespace Narojay.Blog.Models.Dto
{
    public class CreateOrderCommandHandle : IRequestHandler<CreateOrderCommand, string>
    {
        public Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var s = $"CreateOrderCommandHandler: Create Order {request.OrderName}";
            throw new FriendlyException("asdasd");
        }
    }
}
