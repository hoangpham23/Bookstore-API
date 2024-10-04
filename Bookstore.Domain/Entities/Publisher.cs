using System;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class Publisher : BaseEntity
{
    public required string PublisherName { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
