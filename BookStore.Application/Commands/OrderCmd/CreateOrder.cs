using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.OrderCmd;

public class CreateOrder : IRequest<CustOrderDTO>
{
    public required string CustomerId { get; set; }
    public required string DestAddressId { get; set; }
    public required string ShippingMethodId { get; set; }
    public required List<string> BookIds { get; set; }
    
}
