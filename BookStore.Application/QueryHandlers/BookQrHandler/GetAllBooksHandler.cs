using AutoMapper;
using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.QueryHandlers;

public class GetAllBooksHandler : IRequestHandler<GetAllBooks, BasePaginatedList<BookDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private const int PAGE_SIZE = 20;
	public GetAllBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<BasePaginatedList<BookDTO>> Handle(GetAllBooks request, CancellationToken cancellationToken)
	{
		var bookRepo = _unitOfWork.GetRepository<Book>();

		IQueryable<Book> query = bookRepo.Entities
			.Include(b => b.Language)
			.Include(b => b.OrderLines)
            .OrderBy(b => b.Title != null && b.Title.Substring(0, 1).ToLower().CompareTo("a") >= 0 &&
                           b.Title.Substring(0,1).ToLower().CompareTo("z") <= 0 ? 0 : 1)
			.ThenBy(b => b.Title);

		var paginatedBooks = await bookRepo.GetPagging(query, request.Index, PAGE_SIZE);

		var bookDTOs = _mapper.Map<IReadOnlyCollection<BookDTO>>(paginatedBooks.Items);

		return new BasePaginatedList<BookDTO>(bookDTOs, paginatedBooks.TotalItems, request.Index, PAGE_SIZE);
	}
}
