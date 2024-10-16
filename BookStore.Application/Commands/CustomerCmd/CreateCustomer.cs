using System.ComponentModel.DataAnnotations;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.CustomerCmd;

public class CreateCustomer : IRequest<CustomerDTO>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string StreetNumber { get; set; }
    public required string StreetName { get; set; }
    public required string City { get; set; }
    public required string CountryId { get; set; }

}
