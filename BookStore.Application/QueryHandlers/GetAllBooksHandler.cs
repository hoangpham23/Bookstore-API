using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Infrastructure;
using BookStore.Application.DTOs;
using BookStore.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers;

public class GetAllBooksHandler : IRequestHandler<GetAllBooks, IList<BookDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public GetAllBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IList<BookDTO>> Handle(GetAllBooks request, CancellationToken cancellationToken)
	{
		var bookRepo = _unitOfWork.GetRepository<Book>();
		var books = await bookRepo.GetAllAsync(query => query.Include(b => b.Language));

		var bookDTOs = _mapper.Map<IList<BookDTO>>(books);

		return bookDTOs;
	}
}
