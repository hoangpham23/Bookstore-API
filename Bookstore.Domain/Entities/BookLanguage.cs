namespace Bookstore.Domain.Entites;

public partial class BookLanguage
{
    public string LanguageId { get; set; } = null!;

    public string? LanguageCode { get; set; }

    public string? LanguageName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
