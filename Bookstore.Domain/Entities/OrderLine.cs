using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entites;

public partial class OrderLine
{
    [Key]
    public string LineId { get; set; } = Guid.NewGuid().ToString("N");

    public string? OrderId { get; set; }

    public string? BookId { get; set; }

    public decimal? Price { get; set; }

    public virtual Book? Book { get; set; }

    public virtual CustOrder? Order { get; set; }
}
