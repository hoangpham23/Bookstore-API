namespace BookStore.Application.DTOs;

public class CustomerSummaryDTO
{
    public required string CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
