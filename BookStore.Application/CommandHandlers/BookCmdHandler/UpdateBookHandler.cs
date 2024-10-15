using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.CommandHandlers;

public class UpdateBookHandler : IRequestHandler<UpdateBook, BookDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookDTO> Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        try
        {
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var existingBook = await bookRepo.Entities
                            .Include(b => b.Authors).FirstOrDefaultAsync(b => b.BookId == request.BookId);
            if (existingBook == null) throw new ArgumentException("The book doesn't exist");
            
            _mapper.Map(request, existingBook);
            existingBook.Authors.Clear();

            var authorRepo = _unitOfWork.GetRepository<Author>();
            foreach (var authorId in request.AuthorIds)
            {
                var author = await authorRepo.GetByIdAsync(authorId);
                if (author != null)
                {
                    existingBook.Authors.Add(author);
                }
            }

            await bookRepo.UpdateAsync(existingBook);
            await _unitOfWork.SaveChangeAsync();

            var BookDTO = _mapper.Map<BookDTO>(existingBook);
            return BookDTO;
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("Error at the update book handler: " + ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
