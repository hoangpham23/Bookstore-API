using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.ShippingQr;

public class GetAllShippingMethod : IRequest<List<ShippingDTO>>
{

}
