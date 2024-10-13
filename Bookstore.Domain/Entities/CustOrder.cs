namespace Bookstore.Domain.Entites;

public partial class CustOrder
{
    public string OrderId { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public string? CustomerId { get; set; }

    public string? ShippingMethodId { get; set; }

    public string? DestAddressId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Address? DestAddress { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    public virtual ShippingMethod? ShippingMethod { get; set; }
}
