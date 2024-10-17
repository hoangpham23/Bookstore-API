using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.OrderHistoryQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.OrderHistoryQrHandler;

public class GetAllOrderHistoryHandler : IRequestHandler<GetAllOrderHistory, BasePaginatedList<OrderHistoryDTO>>
{
    private const int PAGE_SIZE = 20;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrderHistoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BasePaginatedList<OrderHistoryDTO>> Handle(GetAllOrderHistory request, CancellationToken cancellationToken)
    {
        var custOrderRepo = _unitOfWork.GetRepository<CustOrder>();
        var orderHistoryRepo = _unitOfWork.GetRepository<OrderHistory>();

        var order = await custOrderRepo.FindByConditionAsync(co => co.CustomerId == request.CustomerId);
        if (order == null) throw new KeyNotFoundException("This customer doesn't have order information");

        // var orderHistory = await orderHistoryRepo.FindByConditionAsync(o => o.OrderId == order.OrderId);
        IQueryable<OrderHistory> query =  orderHistoryRepo.Entities
                                        .Include(o => o.Order).ThenInclude(co => co.OrderLines)
                                        .Where(oh => oh.OrderId == order.OrderId && oh.Status.StatusId == request.OrderStatus);
        var paginatedListOrderHistory = await orderHistoryRepo.GetPagging(query, request.Index, PAGE_SIZE);

        var orderHistoryDTO = _mapper.Map<IReadOnlyCollection<OrderHistoryDTO>>(paginatedListOrderHistory.Items);
        return new BasePaginatedList<OrderHistoryDTO>(orderHistoryDTO, paginatedListOrderHistory.TotalItems, request.Index, PAGE_SIZE);
    }
}
