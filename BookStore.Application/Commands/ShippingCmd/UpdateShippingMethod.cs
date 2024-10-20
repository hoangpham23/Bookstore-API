using System.Text.Json.Serialization;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.ShippingCmd;

public class UpdateShippingMethod : IRequest<ShippingDTO>
{
    [JsonIgnore]
    public string? MethodId { get; set; }
    public required string MethodName { get; set; }
    public decimal Cost { get; set; }
}
