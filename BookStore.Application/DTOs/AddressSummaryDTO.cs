namespace BookStore.Application.DTOs;

public class AddressSummaryDTO
{
    public string AddressId { get; set; } = null!;

    public string? StreetNumber { get; set; }

    public string? StreetName { get; set; }

    public string? City { get; set; }

    public string? CountryName { get; set; }
}
