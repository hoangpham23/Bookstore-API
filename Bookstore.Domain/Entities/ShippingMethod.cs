using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class ShippingMethod : BaseEntity
{
    [StringLength(100)]
    public required string MethodName { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Cost { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();
}
