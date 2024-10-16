﻿namespace Bookstore.Domain.Entites;

public partial class OrderHistory
{
    public string HistoryId { get; set; } = Guid.NewGuid().ToString("N");

    public required string OrderId { get; set; }

    public required int StatusId { get; set; }

    public required DateTime StatusDate { get; set; }

    public virtual CustOrder? Order { get; set; }

    public virtual OrderStatus? Status { get; set; }
}
