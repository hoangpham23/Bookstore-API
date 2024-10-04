using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Domain.Entities;

public class AddressStatus
{
    [Key]
    public int Id { get; set; }
    [Column("status_value")]
    public string? StatusValue { get; set; }
}
