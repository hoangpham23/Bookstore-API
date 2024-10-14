using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entites;

public partial class BookLanguage
{
    [Key]
    public string LanguageId { get; set; } = Guid.NewGuid ().ToString("N");

    public string? LanguageCode { get; set; }

    public string? LanguageName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
