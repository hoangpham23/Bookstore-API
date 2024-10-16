using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.CustomerQr;

public class SearchCustomer : IRequest<BasePaginatedList<CustomerDTO>>
{
    public int Index { get; set; }
    public string? CustomerName { get; set; }
}
