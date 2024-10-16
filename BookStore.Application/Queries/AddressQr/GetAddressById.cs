using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.AddressCmd;

public record GetAddressById (string AddressId) : IRequest<AddressDTO>;
