namespace BookStore.Application.DTOs;

public class AddressDTO
{
    
    public required string AddressId { get; set; } 

    public string? StreetNumber { get; set; }

    public string? StreetName { get; set; }

    public string? City { get; set; }

    public string? CountryName { get; set; }
    public List<CustomerSummaryDTO> Customers{ get; set; } = new ();
}
