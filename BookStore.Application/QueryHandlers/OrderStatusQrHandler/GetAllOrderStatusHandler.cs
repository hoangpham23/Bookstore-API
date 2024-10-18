using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.OrderStatusQr;
using MediatR;

namespace BookStore.Application.QueryHandlers.OrderStatusQrHandler;

public class GetAllOrderStatusHandler : IRequestHandler<GetAllOrderStatus, List<OrderStatusDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrderStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<OrderStatusDTO>> Handle(GetAllOrderStatus request, CancellationToken cancellationToken)
    {
        var orderStatusRepo = _unitOfWork.GetRepository<OrderStatus>();
        var orderStatus = await orderStatusRepo.GetAllAsync(null);
        return _mapper.Map<List<OrderStatusDTO>>(orderStatus);
    }
}
