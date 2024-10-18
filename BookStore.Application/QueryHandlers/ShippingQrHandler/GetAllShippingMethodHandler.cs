using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.ShippingQr;
using MediatR;

namespace BookStore.Application.QueryHandlers.ShippingQrHandler;

public class GetAllShippingMethodHandler : IRequestHandler<GetAllShippingMethod, List<ShippingDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllShippingMethodHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ShippingDTO>> Handle(GetAllShippingMethod request, CancellationToken cancellationToken)
    {
        var shippingRepo = _unitOfWork.GetRepository<ShippingMethod>();
        var shippingList = await shippingRepo.GetAllAsync(null) ?? throw new KeyNotFoundException("The shpping list is empty");
        return _mapper.Map<List<ShippingDTO>>(shippingList);
    }
}
