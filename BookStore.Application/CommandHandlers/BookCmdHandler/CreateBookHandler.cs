using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers;

public class CreateBookHandler : IRequestHandler<CreateBook, BookDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BookDTO> Handle(CreateBook request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var bookRepo = _unitOfWork.GetRepository<Book>();
            BookLanguage bookLanguage = await GetOrCreateBookLanguage(request);
            Publisher publisher = await GetOrCreatePublisher(request);
            // await SaveOrderLineAsync(request);

            // mapping the book
            var newBook = _mapper.Map<Book>(request);
            newBook.Authors = await GetOrCreateAuthors(request);
            newBook.Publisher = publisher;
            newBook.Language = bookLanguage;

            await bookRepo.InsertAsync(newBook);
            await _unitOfWork.SaveChangeAsync();

            await _unitOfWork.SaveChangeAsync();

            _unitOfWork.CommitTransaction();

            var bookDTO = _mapper.Map<BookDTO>(newBook);
            bookDTO.AuthorNames = newBook.Authors.Where(a => !string.IsNullOrEmpty(a.AuthorName)) // Filter out null or empty author names
                                    .Select(x => x.AuthorName!).ToList();

            return bookDTO;
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

    // if the book language id exist then retrieve the book language information
    // if don't create a new record
    private async Task<BookLanguage> GetOrCreateBookLanguage(CreateBook request)
    {
        var languageRepo = _unitOfWork.GetRepository<BookLanguage>();
        if (!string.IsNullOrEmpty(request.LanguageId))
        {

            var bookLanguage = await languageRepo.GetByIdAsync(request.LanguageId);
            if (bookLanguage == null)
            {
                throw new ArgumentException("Language information is not found");
            }
            return bookLanguage;
        }
        if (string.IsNullOrEmpty(request.LanguageName) || string.IsNullOrEmpty(request.LanguageCode))
        {

            throw new ArgumentException("Language name and code information is required");
        }

        var newBookLanguage = new BookLanguage
        {
            LanguageName = request.LanguageName,
            LanguageCode = request.LanguageCode
        };
        await languageRepo.InsertAsync(newBookLanguage);

        return newBookLanguage;
    }


    // if the publisher id exist then retrieve the pbulisher information
    // if don't create a new record
    private async Task<Publisher> GetOrCreatePublisher(CreateBook request)
    {
        try
        {
            var publisherRepo = _unitOfWork.GetRepository<Publisher>();
            if (!string.IsNullOrEmpty(request.PublisherId))
            {
                var publisher = await publisherRepo.GetByIdAsync(request.PublisherId);
                if (publisher == null)
                {
                    throw new ArgumentException("Publisher is not found");

                }
                return publisher;
            }

            if (string.IsNullOrEmpty(request.PublisherName))
            {
                throw new ArgumentException("Publisher information is required");
            }

            var newPublisher = new Publisher
            {
                PublisherName = request.PublisherName,
            };

            await publisherRepo.InsertAsync(newPublisher);
            return newPublisher;
        }
        catch (Exception ex)
        {
            throw new Exception("Error when saving the publisher entity: " + ex.Message);
        }

    }

    // if the author id exist then retrieve the author information
    // if don't create a new record
    private async Task<IList<Author>> GetOrCreateAuthors(CreateBook request)
    {
        var authorRepo = _unitOfWork.GetRepository<Author>();
        IList<Author> existingAuthors = new List<Author>();

        if (request.AuthorIds is not null)
        {
            existingAuthors = await authorRepo.GetAllAsync(query => query.Where(a => request.AuthorIds.Contains(a.AuthorId)));
            if (existingAuthors.Count != request.AuthorIds.Count)
            {
                throw new ArgumentException("Some authors don't exist");
            }
        }

        // add the new author and the old author into database
        var newAuthors = new List<Author>();
        if (request.NewAuthorNames != null && request.NewAuthorNames.Any())
        {
            foreach (var authorName in request.NewAuthorNames)
            {
                var author = new Author { AuthorName = authorName };
                newAuthors.Add(author);
                await authorRepo.InsertAsync(author);
            }
        }
        return existingAuthors.Concat(newAuthors).ToList();
    }


}
