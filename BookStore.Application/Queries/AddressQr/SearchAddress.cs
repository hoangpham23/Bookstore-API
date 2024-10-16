using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.AddressCmd;

public class SearchAddress : IRequest<BasePaginatedList<AddressDTO>>
{
    public int Index { get; set; }
    public string? StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string? City { get; set; }
    public string? CountryName { get; set; }
    public string? OrderBy { get; set; }
} 
