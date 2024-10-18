using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.ShippingCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.ShippingMethodHandler;

public class CreateShippingMethodHandler : IRequestHandler<CreateShippingMethod, ShippingDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateShippingMethodHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShippingDTO> Handle(CreateShippingMethod request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var shippingRepo = _unitOfWork.GetRepository<ShippingMethod>();
            var shipping = _mapper.Map<ShippingMethod>(request);
            
            await shippingRepo.InsertAsync(shipping);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<ShippingDTO>(shipping);
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("An error occurred while creating a new shipping method: " + ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
