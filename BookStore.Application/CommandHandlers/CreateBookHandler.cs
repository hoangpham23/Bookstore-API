using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Infrastructure;
using BookStore.Application.Commands;
using MediatR;

namespace BookStore.Application.CommandHandlers;

public class CreateBookHandler : IRequestHandler<CreateBook, Book>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Book> Handle(CreateBook request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var bookRepo = _unitOfWork.GetRepository<Book>();
            var publisherRepo = _unitOfWork.GetRepository<Publisher>();
            var languageRepo = _unitOfWork.GetRepository<BookLanguage>();
            var orderLineRepo = _unitOfWork.GetRepository<OrderLine>();
            var authorRepo = _unitOfWork.GetRepository<Author>();

            // add the new price for the new book into the table orderLine
            await orderLineRepo.InsertAsync(new OrderLine
            {
                Price = request.Price,
            });

            BookLanguage? bookLanguage = null;
            Publisher? publisher = null;

            // add the language id if it exist or new
            if (request.LanguageId.HasValue)
            {
                bookLanguage = await languageRepo.GetByIdAsync(request.LanguageId);
                if (bookLanguage == null)
                {
                    throw new ArgumentException("Language information is not found");
                }
            }
            else if (!string.IsNullOrEmpty(request.LanguageName) && !string.IsNullOrEmpty(request.LanguageCode))
            {
                bookLanguage = new BookLanguage
                {
                    LanguageName = request.LanguageName,
                    LanguageCode = request.LanguageCode
                };
                await languageRepo.InsertAsync(bookLanguage);
            }

            if (bookLanguage == null)
            {
                throw new ArgumentException("Language information is required");
            }

            // add the publisher id if it exist or new
            if (request.PublisherId.HasValue)
            {
                publisher = await publisherRepo.GetByIdAsync(request.PublisherId);
                if (publisher == null)
                {
                    throw new ArgumentException("Publisher is not found");
                }
            }
            else if (!string.IsNullOrEmpty(request.PublisherName))
            {
                publisher = new Publisher
                {
                    PublisherName = request.PublisherName
                };
                await publisherRepo.InsertAsync(publisher);
            }
            if (publisher == null)
            {
                throw new ArgumentException("Publisher information is required");
            }

            // mapping the book
            var newBook = _mapper.Map<Book>(request);

            if (request.AuthorIds == null || request.NewAuthorNames == null)
            {
                throw new ArgumentException("Author name is required");
            }

            // check the existing author list whether it has existing author or not
            IList<Author> existingAuthors = new List<Author>();

            if (request.AuthorIds is not null)
            {
                existingAuthors = await authorRepo.GetAllAsync(query => query.Where(a => request.AuthorIds.Contains(a.AuthorId)));
            }

            List<int>? missingAuthors = request.AuthorIds?.Except(existingAuthors.Select(a => a.AuthorId)).ToList();
            if (missingAuthors != null)
            {
                throw new ArgumentException("Some authors do not exist.");
            }

            // add the new author and the old author into database
            var newAuthors = new List<Author>();
            if (request.NewAuthorNames != null && request.NewAuthorNames.Any())
            {
                foreach (var authorName in request.NewAuthorNames)
                {
                    newAuthors.Add(new Author { AuthorName = authorName });
                }
            }
            newBook.Authors = existingAuthors.Concat(newAuthors).ToList();

            // save the book into database
            await bookRepo.InsertAsync(newBook);
            await _unitOfWork.SaveChangeAsync();

            _unitOfWork.CommitTransaction();
            return newBook;
        }
        catch (System.Exception)
        {
            _unitOfWork.RollBack();
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
