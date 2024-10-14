using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.AuthorQr;

public record  SearchAuthor (
    int Index,
    string? AuthorName
) : IRequest<BasePaginatedList<AuthorDTO>>;
