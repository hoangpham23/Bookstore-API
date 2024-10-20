using System.Text.Json.Serialization;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.OrderStatusCmd;

public class UpdateOrderStatus : IRequest<OrderStatusDTO>
{
    [JsonIgnore]  // This will exclude it from JSON serialization
    public int StatusId { get; set; }
    public required string StatusValue { get; set; }
}
