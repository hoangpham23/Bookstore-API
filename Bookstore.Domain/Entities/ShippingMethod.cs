namespace Bookstore.Domain.Entites;

public partial class ShippingMethod
{
    public string MethodId { get; set; } = null!;

    public string? MethodName { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();
}
