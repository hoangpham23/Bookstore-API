using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class CustomerAddress : BaseEntity
{
    [Column("customer_id")]
    public required string CustomerId { get; set; }

    [Column("address_id")]
    public required string AddressId { get; set; }
    
    [Column("status_id")]
    public int StatusId { get; set; }
    
    public virtual Customer? Customer{ get; set; } = null!;
    public virtual Address Address{ get; set; } = null!;
}
