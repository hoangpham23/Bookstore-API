using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.CustomerQr;

public class GetCustomerById : IRequest<CustomerDTO>
{
    public required string CustomerId { get; set; }
}
