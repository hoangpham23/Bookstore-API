using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookstore.Domain.Entities;

public class Customer : BaseEntity
{
    [StringLength(100)]
    [Column("first_name")]
    public required string FirstName { get; set; }

    [StringLength(100)]
    [Column("last_name")]
    public required string LastName { get; set; }    

    [StringLength(100)]
    public required string Email { get; set; }

     public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
