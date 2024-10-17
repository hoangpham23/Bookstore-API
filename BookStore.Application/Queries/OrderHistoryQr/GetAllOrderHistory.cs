using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.OrderHistoryQr;

public class GetAllOrderHistory : IRequest<BasePaginatedList<OrderHistoryDTO>>
{
    public int Index { get; set; }
    public required string CustomerId { get; set; }
    public int OrderStatus { get; set; }
}
