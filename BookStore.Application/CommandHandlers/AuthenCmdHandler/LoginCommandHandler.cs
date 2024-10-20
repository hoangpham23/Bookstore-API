using System.Diagnostics;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.AuthenCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.AuthenCmdHandler;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _tokenService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<LoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        var customer = await customerRepo.FindByConditionAsync(c => c.Email.ToLower() == request.Email.ToLower() 
                                        && c.FirstName.ToLower() == request.FirstName.ToLower());
        if (customer == null) throw new KeyNotFoundException("Doesn't exist this user");

        var loginDTO = new LoginDTO
        {
            Token = _tokenService.GenerateToken(customer)
        };

        return loginDTO;
    }
}
