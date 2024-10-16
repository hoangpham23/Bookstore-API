namespace BookStore.Application.DTOs;

public class CustomerDTO
{
    public required string CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public List<AddressSummaryDTO> Addresses { get; set; } = new();

}
