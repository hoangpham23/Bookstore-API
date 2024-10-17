namespace BookStore.Application.DTOs;

public class ShippingDTO
{
    public required string MethodId { get; set; }
    public required string MethodName { get; set; }
    public decimal Cost { get; set; }
}
