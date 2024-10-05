using System;
using System.Collections.Generic;

namespace Bookstore.Infrastructure;

public partial class ShippingMethod
{
    public int MethodId { get; set; }

    public string? MethodName { get; set; }

    public decimal? Cost { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();
}
