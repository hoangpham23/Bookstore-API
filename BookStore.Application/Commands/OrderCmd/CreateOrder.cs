using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.OrderCmd;

public class CreateOrder : IRequest<CustOrderDTO>
{
    public required string CustomerId { get; set; }
    public required string AddressId { get; set; }
    public required string ShippingMethodId { get; set; }
    public required string BookId { get; set; }
    
}
