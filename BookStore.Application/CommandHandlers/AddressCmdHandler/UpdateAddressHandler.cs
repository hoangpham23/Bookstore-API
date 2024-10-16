using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.AddressCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.Application.CommandHandlers.AddressCmdHandler;

public class UpdateAddressHandler : IRequestHandler<UpdateAddress, AddressDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAddressHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddressDTO> Handle(UpdateAddress request, CancellationToken cancellationToken)
    {
        try{
_unitOfWork.BeginTransaction();
        var addressRepo = _unitOfWork.GetRepository<Address>();
        var countryRepo = _unitOfWork.GetRepository<Country>();
        IQueryable<Address> query =  addressRepo.Entities
                                                .Include(a => a.CustomerAddresses)
                                                .ThenInclude(ca => ca.Customer);

        var address = query.FirstOrDefault(a => a.AddressId == request.AddressId) 
                            ?? throw new KeyNotFoundException("The address doesn't exist");
        var country = await countryRepo.GetByIdAsync(request.CountryId) 
                            ?? throw new KeyNotFoundException("The country doesn't exist");

        _mapper.Map(request, address);
        address.Country = country;

        await addressRepo.UpdateAsync(address);
        await _unitOfWork.SaveChangeAsync();
        _unitOfWork.CommitTransaction();

        return _mapper.Map<AddressDTO>(address);
        }
        catch(Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("Error when updating the address entity: " + ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
        
    }
}
