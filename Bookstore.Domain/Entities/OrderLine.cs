namespace Bookstore.Domain.Entites;

public partial class OrderLine
{
    public string LineId { get; set; } = null!;

    public string? OrderId { get; set; }

    public string? BookId { get; set; }

    public decimal? Price { get; set; }

    public virtual Book? Book { get; set; }

    public virtual CustOrder? Order { get; set; }
}
