using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.ShippingCmd;

public class CreateShippingMethod : IRequest<ShippingDTO>
{
    public required string MethodName { get; set; }
    public decimal Cost { get; set; }
}
