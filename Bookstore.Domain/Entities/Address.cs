using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class Address : BaseEntity
{
    [StringLength(10)]
    [Column("street_number")]
    public required string StreetNumber { get; set; }

    [StringLength(200)]
    [Column("street_name")]
    public required string StreetName { get; set; }
    
    [StringLength(100)]
    public required string City { get; set; }

    [Column("country_id")]
    public required string CountryId { get; set; }
    public virtual Country? Country { get; set; }
    public virtual ICollection<CustOrder> CustOrders{ get; set; } = new List<CustOrder>();
    public virtual ICollection<CustomerAddress> CustomerAddresses {get; set;} = new List<CustomerAddress>();
}
