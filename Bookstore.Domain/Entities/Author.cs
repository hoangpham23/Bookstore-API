using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entites;

public partial class Author
{
    [Key]
    public string AuthorId { get; set; } = Guid.NewGuid().ToString("N");

    public required string AuthorName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();


}
