using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entites;

public partial class Country
{
    [Key]
    public string CountryId { get; set; } = Guid.NewGuid().ToString("N");

    public string? CountryName { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
