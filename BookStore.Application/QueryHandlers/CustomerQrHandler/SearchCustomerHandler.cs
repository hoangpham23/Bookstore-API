using System;
using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.CustomerQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.CustomerQrHandler;

public class SearchCustomerHandler : IRequestHandler<SearchCustomer, BasePaginatedList<CustomerDTO>>
{
    private const int PAGE_SIZE = 20;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BasePaginatedList<CustomerDTO>> Handle(SearchCustomer request, CancellationToken cancellationToken)
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        IQueryable<Customer> query = customerRepo.Entities.Include(c => c.CustomerAddresses)
                                .ThenInclude(ca => ca.Address).ThenInclude(a => a.Country);
        
        if (!string.IsNullOrEmpty(request.CustomerName))
        {
            query = query.Where(c => c.FirstName != null && c.FirstName.Contains(request.CustomerName));
        }
        query = query.OrderBy(c => c.FirstName);
        
        var paginatedCustomer = await customerRepo.GetPagging(query, request.Index, PAGE_SIZE);
        var customerDTO = _mapper.Map<IReadOnlyCollection<CustomerDTO>>(paginatedCustomer.Items);
        return new BasePaginatedList<CustomerDTO>(customerDTO, paginatedCustomer.TotalItems, request.Index, PAGE_SIZE);
    }
}
