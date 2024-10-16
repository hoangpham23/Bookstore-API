using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Core.Store;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.AddressCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.CommandHandlers.AddressCmdHandler;

public class SearchAddressHandler : IRequestHandler<SearchAddress, BasePaginatedList<AddressDTO>>
{
    private const int PAGE_SIZE = 20;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchAddressHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BasePaginatedList<AddressDTO>> Handle(SearchAddress request, CancellationToken cancellationToken)
    {
        var addressRepo = _unitOfWork.GetRepository<Address>();
        IQueryable<Address> query = addressRepo.Entities
                                        .Include(ca => ca.CustomerAddresses)
                                        .ThenInclude(c => c.Customer);

        if (!string.IsNullOrEmpty(request.StreetName))
        {
            query = query.Where(a => a.StreetName != null && a.StreetName.Contains(request.StreetName));
        }

        if (!string.IsNullOrEmpty(request.StreetNumber))
        {
            query = query.Where(a => a.StreetNumber != null && a.StreetNumber.Contains(request.StreetNumber));
        }

        if (!string.IsNullOrEmpty(request.City))
        {
            query = query.Where(a => a.City != null && a.City.Contains(request.City));
        }

        if (!string.IsNullOrEmpty(request.CountryName))
        {
            query = query.Where(a => a.Country != null && a.Country.CountryName != null
                                && a.Country.CountryName.Contains(request.CountryName));
        }

        switch (request.OrderBy?.ToLower())
        {
            case "streetnumber":
                query = query.OrderBy(a => a.StreetNumber);
                break;
            case "streetname":
                query = query.OrderBy(a => a.StreetName);
                break;
            case "countryname":
                query = query.OrderBy(a => a.Country.CountryName);
                break;
            case "city":
                query = query.OrderBy(a => a.City);
                break;
            default:
                query = query.OrderBy(a => (a.StreetName.Substring(0, 1).ToLower().CompareTo("a") >= 0 &&
                                    a.StreetName.Substring(0, 1).ToLower().CompareTo("z") <= 0) ? 0 : 1).ThenBy(a => a.StreetName);
                break;
        }

        var paginatedAddress = await addressRepo.GetPagging(query, request.Index, PAGE_SIZE);
        var addresDTO = _mapper.Map<IReadOnlyCollection<AddressDTO>>(paginatedAddress.Items);

        return new BasePaginatedList<AddressDTO>(addresDTO, paginatedAddress.TotalItems, request.Index, PAGE_SIZE);
    }
}
