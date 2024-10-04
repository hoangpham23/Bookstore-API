using System.ComponentModel.DataAnnotations;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class Author : BaseEntity
{
    [StringLength(100)]
    public required string AuthorName { get; set; }
    public virtual ICollection<Book> Books{ get; set; } = new List<Book>();
}
