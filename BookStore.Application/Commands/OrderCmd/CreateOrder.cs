using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.OrderCmd;

public class CreateOrder : IRequest<CustOrderDTO>
{
    public string? CustomerId { get; set; }
}
