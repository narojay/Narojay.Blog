﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Narojay.Blog.Domain.Models.Dto;

public class CreateOrderCommandHandle : IRequestHandler<CreateOrderCommand, string>
{
    public Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var s = $"CreateOrderCommandHandler: Create Order {request.OrderName}";
        throw new StringResponseException("asdasd");
    }
}