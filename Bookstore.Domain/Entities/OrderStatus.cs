namespace Bookstore.Domain.Entites;

public partial class OrderStatus
{
    public int StatusId { get; set; }

    public required string StatusValue { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}
