using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.OrderStatusCmd;

public class CreateOrderStatus : IRequest<OrderStatusDTO>
{
    public required string StatusValue { get; set; }
}
