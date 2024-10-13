using System;
using System.Collections.Generic;

namespace Bookstore.Domain.Entites;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string? StreetNumber { get; set; }

    public string? StreetName { get; set; }

    public string? City { get; set; }

    public string? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
