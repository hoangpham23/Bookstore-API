using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.AuthorQr;
using MediatR;

namespace BookStore.Application.QueryHandlers.AuthorQrHandler;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorById, AuthorDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDTO> Handle(GetAuthorById request, CancellationToken cancellationToken)
    {
        var authorRepo = _unitOfWork.GetRepository<Author>();
        var author = await authorRepo.FindByConditionAsync(a => a.AuthorId == request.AuthorId);
        if (author == null) throw new KeyNotFoundException("Author doesn't exist");

        return _mapper.Map<AuthorDTO>(author);
    }
}
