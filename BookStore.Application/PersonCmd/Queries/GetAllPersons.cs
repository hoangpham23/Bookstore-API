using Bookstore.Domain.Entities;
using MediatR;

namespace BookStore.Application.PersonCmd.Queries
{
	public class GetAllPersons : IRequest<IList<Person>>
	{
	}
}
