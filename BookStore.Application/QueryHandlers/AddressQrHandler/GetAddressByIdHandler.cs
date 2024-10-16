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
        var addressRepo = _unitOfWork.GetRepository<Address>();
        IQueryable<Address> query = addressRepo.Entities.Include(ca => ca.CustomerAddresses).ThenInclude(c => c.Customer);
        var address = await query.FirstOrDefaultAsync(a => a.AddressId == request.AddressId);
        if (address == null) throw new KeyNotFoundException("The address doesn't exist");

        return _mapper.Map<AddressDTO>(address);
    }
}
