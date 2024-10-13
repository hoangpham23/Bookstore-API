namespace Bookstore.Domain.Entites;

public partial class OrderHistory
{
    public string HistoryId { get; set; } = null!;

    public string? OrderId { get; set; }

    public int? StatusId { get; set; }

    public DateTime? StatusDate { get; set; }

    public virtual CustOrder? Order { get; set; }

    public virtual OrderStatus? Status { get; set; }
}
