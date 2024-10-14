using System;
using System.Reflection.Metadata;
using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.LanguageQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.LanguageHandler;

public class SearchLanguageHandler : IRequestHandler<SearchLangugae, BasePaginatedList<LanguageDTO>>
{
    private const int PAGE_SIZE = 10;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchLanguageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BasePaginatedList<LanguageDTO>> Handle(SearchLangugae request, CancellationToken cancellationToken)
    {
        var languageRepo = _unitOfWork.GetRepository<BookLanguage>();
        IQueryable<BookLanguage> query = languageRepo.Entities.Include(b => b.Books);

        if (!string.IsNullOrEmpty(request.LanguageName))
        {
            query = query.Where(bl => bl.LanguageName != null && bl.LanguageName.Contains(request.LanguageName));
        }

        if (!string.IsNullOrEmpty(request.LanguageCode)){
            query = query.Where(bl => bl.LanguageCode != null && bl.LanguageCode.Contains(request.LanguageCode));
        }

        query = query.OrderBy(bl => bl.LanguageCode);

        var paginatedLanguage = await languageRepo.GetPagging(query, request.Index, PAGE_SIZE);
        var bookLanguageDTOs = _mapper.Map<IReadOnlyCollection<LanguageDTO>>(paginatedLanguage.Items);

        return new BasePaginatedList<LanguageDTO>(bookLanguageDTOs, paginatedLanguage.TotalItems, request.Index, PAGE_SIZE);
    }
}
