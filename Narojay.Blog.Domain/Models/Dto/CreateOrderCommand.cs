using MediatR;

namespace Narojay.Blog.Domain.Models.Dto;

public class CreateOrderCommand : IRequest<string>
{
    public string OrderName { get; set; }
}