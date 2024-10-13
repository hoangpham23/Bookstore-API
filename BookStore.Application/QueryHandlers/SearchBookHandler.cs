using System;
using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers;

public class SearchBookHandler : IRequestHandler<SearchBook, BasePaginatedList<BookDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private const int PAGE_SIZE = 10;

    public SearchBookHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BasePaginatedList<BookDTO>> Handle(SearchBook request, CancellationToken cancellationToken)
    {
        try
        {
            var bookRepo = _unitOfWork.GetRepository<Book>();
            IQueryable<Book> query = bookRepo.Entities.Include(b => b.Language);
            if (!string.IsNullOrEmpty(request.LanguageName))
            {
                query = query.Where(b => b.Language != null &&
                                        b.Language.LanguageName != null &&
                                        b.Language.LanguageName.Contains(request.LanguageName));
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(b => b.Title != null && b.Title.Contains(request.Title));
            }

            var paginatedBooks = await bookRepo.GetPagging(query, request.Index, PAGE_SIZE);
            var bookDTOs = _mapper.Map<IReadOnlyCollection<BookDTO>>(paginatedBooks.Items);

            return new BasePaginatedList<BookDTO>(bookDTOs, paginatedBooks.TotalItems, request.Index, PAGE_SIZE);
        }
        catch (Exception ex)
        {
            throw new Exception("Error at the search book handler: " + ex.Message);
        }

    }
}
