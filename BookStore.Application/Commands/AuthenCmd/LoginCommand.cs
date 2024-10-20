using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.AuthenCmd;

public class LoginCommand : IRequest<LoginDTO>
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
}
