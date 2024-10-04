using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class OrderLine : BaseEntity
{
    [Column("order_id")]
    public required string OrderId { get; set; }
    public virtual CustOrder? Order { get; set; }

    [Column("book_id")]
    public required string BookId { get; set; }
    public virtual Book? Book { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Price { get; set; }
}
