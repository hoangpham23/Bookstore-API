using AutoMapper;
using Bookstore.Core.Store;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.OrderCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

    public async Task<CustOrderDTO> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var orderRepo = _unitOfWork.GetRepository<CustOrder>();
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var orderLineRepo = _unitOfWork.GetRepository<OrderLine>();
            var orderHistoryRepo = _unitOfWork.GetRepository<OrderHistory>();

            var order = _mapper.Map<CustOrder>(request);
            order.OrderDate = DateTime.Now;
            await orderRepo.InsertAsync(order);
            await orderRepo.SaveAsync();

            List<OrderBooksDTO> orderBooksDTOs = new();
            foreach (var bookId in request.BookIds)
            {
                var book = await bookRepo.GetByIdAsync(bookId) ?? throw new KeyNotFoundException("The book doesn't exist");
                orderBooksDTOs.Add(_mapper.Map<OrderBooksDTO>(book));
                var orderLine = new OrderLine
                {
                    OrderId = order.OrderId,
                    BookId = book.BookId,
                    Price = book.Price
                };
                await orderLineRepo.InsertAsync(orderLine);
            }

            var orderHistory = new OrderHistory
            {
                OrderId = order.OrderId,
                StatusDate = DateTime.Now,
                StatusId = (int)OrderHistoryStatus.ORDER_RECEIVE,
            };
            await orderHistoryRepo.InsertAsync(orderHistory);

            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            var newOrder = await orderRepo.Entities
                .Include(o => o.Customer)
                .Include(o => o.DestAddress)
                .Include(o => o.ShippingMethod)
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Book)
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if (newOrder == null) throw new KeyNotFoundException("An error occurred while retrieving the new orde");

            CustOrderDTO result = _mapper.Map<CustOrderDTO>(newOrder);
            result.Customer = _mapper.Map<OrderCustomerDTO>(newOrder.Customer);
            result.Customer.Address = _mapper.Map<AddressSummaryDTO>(newOrder.DestAddress);
            result.Shipping = _mapper.Map<ShippingDTO>(newOrder.ShippingMethod);
            result.OrderBooks = _mapper.Map<List<OrderBooksDTO>>(newOrder.OrderLines);
            
            if (newOrder.ShippingMethod == null) throw new KeyNotFoundException("An error occurred while calculate the total price for order");
            result.TotalPrice = newOrder.OrderLines.Sum(ol => ol.Price) + newOrder.ShippingMethod.Cost;
            
            return result;
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
