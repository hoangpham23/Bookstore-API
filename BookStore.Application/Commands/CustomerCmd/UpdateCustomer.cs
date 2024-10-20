using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.CustomerCmd;

public class UpdateCustomer : IRequest<CustomerDTO>
{
    [JsonIgnore]
    public string? CustomerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string AddressId { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string StreetNumber { get; set; }
    public required string StreetName { get; set; }
    public required string City { get; set; }
    public required string CountryId { get; set; }
}
