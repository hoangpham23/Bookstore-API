using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.CustomerQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.CustomerQrHandler;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, CustomerDTO>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDTO> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        var query = customerRepo.Entities.Include(c => c.CustomerAddresses)
                                        .ThenInclude(ca => ca.Address).ThenInclude(a => a.Country);
        
        var customer = await query.FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
        if (customer == null) throw new KeyNotFoundException("The customer doesn't exist");
        
        var customerDTO = _mapper.Map<CustomerDTO>(customer);
        return customerDTO;
    }
}
