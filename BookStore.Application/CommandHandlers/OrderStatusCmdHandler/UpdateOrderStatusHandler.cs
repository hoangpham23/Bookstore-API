using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.OrderStatusCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.Application.CommandHandlers.OrderStatusCmdHandler;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatus, OrderStatusDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderStatusDTO> Handle(UpdateOrderStatus request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var orderStatusRepo = _unitOfWork.GetRepository<OrderStatus>();
            var orderStatus = await orderStatusRepo.GetByIdAsync(request.StatusId) ?? throw new KeyNotFoundException("The order status id doesn't exist");
            
            _mapper.Map(request, orderStatus);
            await orderStatusRepo.UpdateAsync(orderStatus);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }
        catch (Exception ex)
        {
            _unitOfWork.Dispose();
            throw new Exception("An error occurred while updating order status: " + ex.Message);

        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}