

using Bookstore.Core.Base;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entities;
using BookStore.Application.PersonCmd.Queries;
using MediatR;

namespace BookStore.Application.PersonCmd.QueryHandlers
{
	public class GetPersonByIdHandler : IRequestHandler<GetPersonById, Person>
	{
		private readonly IUnitOfWork _unitOfWork;
        public GetPersonByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Person> Handle(GetPersonById request, CancellationToken cancellationToken)
		{
            var personRepo = _unitOfWork.GetRepository<Person>();
			var person =  await personRepo.GetByIdAsync(request.Id);
			if (person == null)
			{
				throw new KeyNotFoundException("Person not found.");
			}

			return person;
        }
	}
}
