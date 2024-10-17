using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entites;

public partial class Book
{
    [Key]
    public string BookId { get; set; } = Guid.NewGuid().ToString("N");

    public required string Title { get; set; }

    public string? Isbn13 { get; set; }

    public string? LanguageId { get; set; }

    public int? NumPages { get; set; }
    public required decimal Price { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public string? PublisherId { get; set; }

    public virtual BookLanguage? Language { get; set; }

    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
