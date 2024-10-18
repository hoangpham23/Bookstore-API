using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.OrderStatusCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.OrderStatusCmdHandler;

public class CreateOrderStatusHandler : IRequestHandler<CreateOrderStatus, OrderStatusDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderStatusDTO> Handle(CreateOrderStatus request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var orderStatusRepo = _unitOfWork.GetRepository<OrderStatus>();
            var orderStatus = _mapper.Map<OrderStatus>(request);
            
            await orderStatusRepo.InsertAsync(orderStatus);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }
        catch (Exception ex)
        {
            _unitOfWork.Dispose();
            throw new Exception("An error occurred while creating a new order status: " + ex.Message);

        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
