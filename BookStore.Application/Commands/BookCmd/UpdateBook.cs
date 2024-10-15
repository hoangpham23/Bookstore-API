using System.Text.Json.Serialization;
using Bookstore.Core.Store;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands;

public class UpdateBook : IRequest<BookDTO>
{
    public string? BookId { get; set; }
    public required string Title { get; set; }
    public required string Isbn13 { get; set; }
    public required decimal Price { get; set; }
    public required string LanguageId { get; set; }
    public required int NumPages { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public required DateOnly PublicationDate { get; set; }
    public required string PublisherId { get; set; }
    public required List<string> AuthorIds { get; set; }

}
