using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.AuthorQr;

public class GetAuthorById : IRequest<AuthorDTO>
{
    public required string AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public List<BookTitleDTO>? BookTitleDTOs{ get; set; }
}
