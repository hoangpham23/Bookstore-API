using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.BookQr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers.BookQrHandler;

public class GetByIdHandler : IRequestHandler<GetBookById, BookDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookDTO> Handle(GetBookById request, CancellationToken cancellationToken)
    {
        var bookRepo = _unitOfWork.GetRepository<Book>();
        var book = await bookRepo.Entities
                        .Include(b => b.Language)
                        .Include(b => b.Publisher)
                        .Include(b => b.OrderLines)
                        .Include(b => b.Authors).FirstOrDefaultAsync(b => b.BookId == request.Id);
        if (book == null) throw new KeyNotFoundException("The book doens't exist");
        return _mapper.Map<BookDTO>(book);
    }
}
