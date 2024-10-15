using System;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.BookQr;

public class GetBookById : IRequest<BookDTO>
{
    public required string Id { get; set; }
}
