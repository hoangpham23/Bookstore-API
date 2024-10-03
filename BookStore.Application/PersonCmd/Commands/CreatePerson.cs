using Bookstore.Domain.Entities;
using MediatR;

namespace Application.PersonCmd.Commands;

public class CreatePerson : IRequest<Person>
{
	public string? Name { get; set; }
	public string? Email { get; set; }
};