using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Domain.Entites;

public partial class ShippingMethod
{
    [Key]
    public string MethodId { get; set; } = Guid.NewGuid().ToString("N");

    public string? MethodName { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();
}
