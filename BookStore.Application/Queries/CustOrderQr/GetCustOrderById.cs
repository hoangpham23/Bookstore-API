using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.QueryHandlers.CustOrderQrHandler;

public class GetCustOrderById : IRequest<CustOrderDTO>
{
    public required string OrderId { get; set; }
}
