using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.ShippingCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.ShippingMethodHandler;

public class UpdateShippingMethodHandler : IRequestHandler<UpdateShippingMethod, ShippingDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateShippingMethodHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShippingDTO> Handle(UpdateShippingMethod request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var shippingRepo = _unitOfWork.GetRepository<ShippingMethod>();
            if (request.MethodId == null) throw new ArgumentException("The shipping method ID is required.");
            var shipping = await shippingRepo.GetByIdAsync(request.MethodId) ?? throw new KeyNotFoundException("The shipping method ID doesn't exist");
            
            _mapper.Map(request, shipping);
            await shippingRepo.UpdateAsync(shipping);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<ShippingDTO>(shipping);
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("An error occurred while creating a new shipping method: "+ ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
