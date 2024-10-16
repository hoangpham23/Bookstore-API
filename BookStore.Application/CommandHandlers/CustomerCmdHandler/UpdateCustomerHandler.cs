using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.CustomerCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.CommandHandlers.CustomerCmdHandler;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer, CustomerDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDTO> Handle(UpdateCustomer request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var customerRepo = _unitOfWork.GetRepository<Customer>();
            if (request.CustomerId == null) throw new ArgumentException("The customerId is required");

            var query = customerRepo.Entities.Include(c => c.CustomerAddresses).ThenInclude(a => a.Address);
            var existingCustomer = await query.FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
            if (existingCustomer == null) throw new KeyNotFoundException("The customer doesn't exist");

            var customerAddress = existingCustomer.CustomerAddresses.FirstOrDefault(ca => ca.AddressId == request.AddressId);
            if (customerAddress == null) throw new InvalidOperationException("The specified address is not associated with the given customer.");

            _mapper.Map(request, existingCustomer);
            _mapper.Map(request, customerAddress.Address);

            await customerRepo.UpdateAsync(existingCustomer);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<CustomerDTO>(existingCustomer);
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("Error while updating the customer: " + ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
