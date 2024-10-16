using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.CustomerCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.CustomerCmdHandler;

public class CreateCustomerHandler : IRequestHandler<CreateCustomer, CustomerDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDTO> Handle(CreateCustomer request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            var customerRepo = _unitOfWork.GetRepository<Customer>();
            var addressRepo = _unitOfWork.GetRepository<Address>();
            var countryRepo = _unitOfWork.GetRepository<Country>();
            
            var customer = _mapper.Map<Customer>(request);
            var address = _mapper.Map<Address>(request);
            address.Country = await countryRepo.GetByIdAsync(request.CountryId);
            
            await addressRepo.InsertAsync(address);
            customer.CustomerAddresses.Add(new CustomerAddress {Address = address});
            await customerRepo.InsertAsync(customer);
            
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<CustomerDTO>(customer);
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("Error while saving the customer entity: " + ex.Message);
        }
        finally{
            _unitOfWork.Dispose();
        }
    }
}
