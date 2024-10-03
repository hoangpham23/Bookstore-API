using Bookstore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.PersonCmd.Queries
{
	public class GetPersonById : IRequest<Person>
	{
        public int Id { get; set; }
	}
}
