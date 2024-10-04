using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class OrderHistory : BaseEntity
{
    [Column("order_id")]
    public required string OrderId { get; set; }
    public virtual CustOrder? Order { get; set; }
    [Column("status_date")]
    public DateTime? StatusDate { get; set; }
    [Column("status_id")]
    public int StatusId { get; set; }
    public virtual OrderStatus? OrderStatus { get; set; }
}
