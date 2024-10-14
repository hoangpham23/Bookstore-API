using System;
using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.AuthorQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.AuthorQrHandler;

public class SearchAuthorHandler : IRequestHandler<SearchAuthor, BasePaginatedList<AuthorDTO>>
{
    private const int PAGE_SIZE = 20;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BasePaginatedList<AuthorDTO>> Handle(SearchAuthor request, CancellationToken cancellationToken)
    {
        var authorRepo = _unitOfWork.GetRepository<Author>();
        IQueryable<Author> query = authorRepo.Entities.Include(a => a.Books);

        if (!string.IsNullOrEmpty(request.AuthorName))
        {
            query = query.Where(a => a.AuthorName != null && a.AuthorName.Contains(request.AuthorName));
        }
        query = query.OrderBy(a => a.AuthorName);
        
        var paginatedAuthor = await authorRepo.GetPagging(query, request.Index, PAGE_SIZE);
        var authorDTOs = _mapper.Map<IReadOnlyCollection<AuthorDTO>>(paginatedAuthor.Items);
        
        return new BasePaginatedList<AuthorDTO>(
            authorDTOs,
            paginatedAuthor.TotalItems,
            request.Index,
            PAGE_SIZE
        );
    }
}
