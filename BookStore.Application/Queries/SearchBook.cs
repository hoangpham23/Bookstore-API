using System;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries;

public class SearchBook : IRequest<IList<BookDTO>>
{
    public string? Title { get; set; }
    public string? LanguageName { get; set; }
}
