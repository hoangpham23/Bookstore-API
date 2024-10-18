using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.AddressCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.CommandHandlers.AddressCmdHandler;

public class GetAddressByIdHandler : IRequestHandler<GetAddressById, AddressDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAddressByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddressDTO> Handle(GetAddressById request, CancellationToken cancellationToken)
    {
        var custAddressRepo = _unitOfWork.GetRepository<CustomerAddress>();
        IQueryable<CustomerAddress> query = custAddressRepo.Entities.Include(ca => ca.Address );
        var custAddress = await query.FirstOrDefaultAsync(ca => ca.CustomerId == request.AddressId);
        if (custAddress == null) throw new KeyNotFoundException("The address doesn't exist");

        return _mapper.Map<AddressDTO>(custAddress.Address);
    }
}
