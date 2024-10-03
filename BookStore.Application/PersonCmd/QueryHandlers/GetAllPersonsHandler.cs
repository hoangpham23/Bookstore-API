using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entities;
using BookStore.Application.PersonCmd.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.PersonCmd.QueryHandlers
{
	public class GetAllPersonsHandler : IRequestHandler<GetAllPersons, IList<Person>>
	{
		private readonly IUnitOfWork _unitOfWork;
        public GetAllPersonsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IList<Person>> Handle(GetAllPersons request, CancellationToken cancellationToken)
		{
			var personRepo = _unitOfWork.GetRepository<Person>();
			return personRepo.GetAllAsync();
		}
	}
}
