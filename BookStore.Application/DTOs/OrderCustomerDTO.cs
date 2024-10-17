namespace BookStore.Application.DTOs;

public class OrderCustomerDTO
{
    public required string CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public AddressSummaryDTO? Address { get; set; } // Only the relevant address
}
