namespace Bookstore.Domain.Entites;

public partial class CustomerAddress
{
    public string CustomerId { get; set; } = null!;

    public string AddressId { get; set; } = null!;

    public int? StatusId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
