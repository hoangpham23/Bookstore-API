using System;
using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries;

public class SearchBook : IRequest<BasePaginatedList<BookDTO>>
{
    public int Index { get; set; }
    public string? Title { get; set; }
    public string? LanguageName { get; set; }
}
