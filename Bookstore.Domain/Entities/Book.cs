namespace Bookstore.Domain.Entites;

public partial class Book
{
    public string BookId { get; set; } = null!;

    public string? Title { get; set; }

    public string? Isbn13 { get; set; }

    public string? LanguageId { get; set; }

    public int? NumPages { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public string? PublisherId { get; set; }

    public virtual BookLanguage? Language { get; set; }

    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
