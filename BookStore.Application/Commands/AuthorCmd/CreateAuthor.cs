using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.AuthorCmd;

public class CreateAuthor : IRequest<AuthorDTO>
{
    public required string AuthorName { get; set; }
}
