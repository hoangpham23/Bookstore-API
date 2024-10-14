using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.LanguageQr;

public record SearchLangugae(
    int Index, 
    string? LanguageName,
    string? LanguageCode
) : IRequest<BasePaginatedList<LanguageDTO>>;