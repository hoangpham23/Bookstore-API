using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entities;

public class OrderStatus
{
    [Key]
    public int Id { get; set; }
    [StringLength(20)]
    public string? StatusValue { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories {get; set; } = new List<OrderHistory>();
}
