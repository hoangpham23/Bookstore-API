using System.Text.Json.Serialization;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.AddressCmd;

public class UpdateAddress : IRequest<AddressDTO>
{
    [JsonIgnore]
    public string? AddressId { get; set; }
    public required string StreetNumber { get; set; }
    public required string StreetName { get; set; }
    public required string City { get; set; }
    public required string CountryId { get; set; }
}
