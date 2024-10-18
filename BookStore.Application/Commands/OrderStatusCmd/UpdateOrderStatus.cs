using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.OrderStatusCmd;

public class UpdateOrderStatus : IRequest<OrderStatusDTO>
{
    public int StatusId { get; set; }
    public required string StatusValue { get; set; }
}
