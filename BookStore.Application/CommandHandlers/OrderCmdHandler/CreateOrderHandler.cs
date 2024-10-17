using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.OrderCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.OrderCmdHandler;

public class CreateOrderHandler : IRequestHandler<CreateOrder, CustOrderDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<CustOrderDTO> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var orderRepo = _unitOfWork.GetRepository<CustOrder>();



            _unitOfWork.CommitTransaction();
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while creating order: " + ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
