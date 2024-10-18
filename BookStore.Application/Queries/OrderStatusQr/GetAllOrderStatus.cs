using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.OrderStatusQr;

public class GetAllOrderStatus : IRequest<List<OrderStatusDTO>>
{
    
}
