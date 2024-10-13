namespace Bookstore.Domain.Entites;

public partial class Country
{
    public string CountryId { get; set; } = null!;

    public string? CountryName { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
