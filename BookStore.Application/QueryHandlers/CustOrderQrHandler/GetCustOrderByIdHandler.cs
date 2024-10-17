using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.QueryHandlers.CustOrderQrHandler;

public class GetCustOrderByIdHandler : IRequestHandler<GetCustOrderById, CustOrderDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustOrderDTO> Handle(GetCustOrderById request, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.GetRepository<CustOrder>();
        var order = await orderRepo.GetByIdAsync(request.OrderId);
        if (order == null) throw new KeyNotFoundException("The order doesn't exist");
        
        CustOrderDTO result = new CustOrderDTO();
        result.Customer = _mapper.Map<OrderCustomerDTO>(order.Customer);
        result.Customer.Address = _mapper.Map<AddressSummaryDTO>(order.DestAddress);
        result.Shipping = _mapper.Map<ShippingDTO>(order.ShippingMethod);
        result.OrderBooks = _mapper.Map<List<OrderBooksDTO>>(order.OrderLines);
        _mapper.Map(order, result);

        return result;
    }
}
