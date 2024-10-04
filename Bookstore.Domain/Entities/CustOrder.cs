using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class CustOrder 
{
    [Column("order_date")]
    public DateTime? OrderDate { get; set; }

    [Column("customer_id")]
    public required string CustomerId { get; set; }
    
    [Column("shipping_method_id")]
    public required string ShippingIMethodId { get; set; } 

    [Column("address_id")]
    public required string AddressId { get; set; }
    
    public virtual Customer? Customer{ get; set; }
    public virtual Address? Address{ get; set; }

    public virtual ShippingMethod? ShippingMethod { get; set; }
    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}
