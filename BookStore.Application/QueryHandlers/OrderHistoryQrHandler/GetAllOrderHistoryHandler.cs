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

        IList<CustOrder> order = await custOrderRepo.GetAllAsync(query => query.Where(co => co.CustomerId == request.CustomerId));
        if (order == null || order.Count == 0) throw new KeyNotFoundException("This customer doesn't have order information");

        var orderIds = order.Select(o => o.OrderId).ToList();

        // Fetch the latest order history entries
        var latestOrderHistories = await orderHistoryRepo.Entities
            .Include(oh => oh.Order).ThenInclude(o => o != null ? o.OrderLines : null)
            .Where(oh => orderIds.Contains(oh.OrderId) && oh.StatusId == request.OrderStatus)
            .OrderByDescending(oh => oh.StatusDate)
            .ToListAsync();

        var orderHistoryFiltered = latestOrderHistories
            .GroupBy(o => o.OrderId)
            .Select(o => o.FirstOrDefault())
            .ToList();


        var paginatedOrderHistory = orderHistoryFiltered
            .Skip((request.Index - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToList();

        var orderHistoryDTO = _mapper.Map<IReadOnlyCollection<OrderHistoryDTO>>(paginatedOrderHistory);
        return new BasePaginatedList<OrderHistoryDTO>(orderHistoryDTO, paginatedOrderHistory.Count, request.Index, PAGE_SIZE);
    }
}
