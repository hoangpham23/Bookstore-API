using Bookstore.Infrastructure;
using MediatR;

namespace BookStore.Application.Commands;

public class CreateBook : IRequest<Book>
{
    public required string Title { get; set; }
    public required string Isbn13 { get; set; }
    public required decimal Price { get; set; }
    public int? LanguageId { get; set; }
    public string? LanguageName { get; set; }
    public string? LanguageCode { get; set; }
    public required int NumPages { get; set; }
    public required DateOnly PublicationDate { get; set; }
    public int? PublisherId { get; set; }
    public string? PublisherName { get; set; }
    public List<int>? AuthorIds { get; set; }
    public List<string>? NewAuthorNames { get; set; }

}
