using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Infrastructure;
using BookStore.Application.DTOs;
using BookStore.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers;

public class SearchBookHandler : IRequestHandler<SearchBook, IList<BookDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchBookHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<BookDTO>> Handle(SearchBook request, CancellationToken cancellationToken)
    {
        var bookRepo = _unitOfWork.GetRepository<Book>();
        IList<Book> books = new List<Book>();
        if (!string.IsNullOrEmpty(request.LanguageName))
        {
            books = await bookRepo.GetAllAsync(query => query
                .Include(b => b.Language)
                .Where(b => b.Language != null && b.Language.LanguageName != null 
                            && b.Language.LanguageName.Contains(request.LanguageName)));
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            books = await bookRepo.GetAllAsync(query => query
                .Where(b => b.Title != null && b.Title.Contains(request.Title)));
        }

        return _mapper.Map<IList<BookDTO>>(books);
    }
}
