using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.PublisherQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.PublisherHandler;

public class SearchPublisherHandler : IRequestHandler<SearchPublisher, BasePaginatedList<PublisherDTO>>
{
    private const int PAGE_SIZE = 20;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchPublisherHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BasePaginatedList<PublisherDTO>> Handle(SearchPublisher request, CancellationToken cancellationToken)
    {
        var publisherRepo = _unitOfWork.GetRepository<Publisher>();

        IQueryable<Publisher> query = publisherRepo.Entities.Include(p => p.Books);
        if (!string.IsNullOrEmpty(request.PublisherName)){
            query = query.Where(p => p.PublisherName != null && p.PublisherName.Contains(request.PublisherName));
        }
        query = query.OrderBy(p => p.PublisherName);
        
        var paginatedPublisher = await publisherRepo.GetPagging(query, request.Index, PAGE_SIZE);
        var publisherDTos = _mapper.Map<IReadOnlyCollection<PublisherDTO>>(paginatedPublisher.Items);
        
        return new BasePaginatedList<PublisherDTO>(
            publisherDTos, 
            paginatedPublisher.TotalItems, 
            request.Index, 
            PAGE_SIZE
        );
    }
}
