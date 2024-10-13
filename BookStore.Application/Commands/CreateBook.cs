using System.Text.Json.Serialization;
using Bookstore.Core.Store;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands;

public class CreateBook : IRequest<BookDTO>
{
    public required string Title { get; set; }
    public required string Isbn13 { get; set; }
    public required decimal Price { get; set; }
    public string? LanguageId { get; set; }
    public string? LanguageName { get; set; }
    public string? LanguageCode { get; set; }
    public required int NumPages { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public required DateOnly PublicationDate { get; set; }
    public string? PublisherId { get; set; }
    public string? PublisherName { get; set; }
    public List<string>? AuthorIds { get; set; }
    public List<string>? NewAuthorNames { get; set; }

}
