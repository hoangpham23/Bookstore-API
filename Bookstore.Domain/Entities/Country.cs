using System.ComponentModel.DataAnnotations;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class Country : BaseEntity
{
    [StringLength(200)]
    public required string CountryName { get; set; }
    public virtual ICollection<Address> Addresses{ get; set; } = new List<Address>();
}
