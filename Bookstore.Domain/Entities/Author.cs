namespace Bookstore.Domain.Entites;

public partial class Author
{
    public string AuthorId { get; set; } = null!;

    public string? AuthorName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
