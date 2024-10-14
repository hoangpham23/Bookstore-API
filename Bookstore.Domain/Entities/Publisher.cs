using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entites;

public partial class Publisher
{
    [Key]
    public string PublisherId { get; set; } = Guid.NewGuid().ToString("N");

    public string? PublisherName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
