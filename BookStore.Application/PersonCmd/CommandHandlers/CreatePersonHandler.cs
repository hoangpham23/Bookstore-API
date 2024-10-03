
using Application.PersonCmd.Commands;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entities;
using MediatR;

namespace BookStore.Application.PersonCmd.CommandHandlers
{
	public class CreatePersonHandler : IRequestHandler<CreatePerson, Person>
	{
		private readonly IUnitOfWork _unitOfWork;

		public CreatePersonHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Person> Handle(CreatePerson request, CancellationToken cancellationToken)
		{
			try
			{
				_unitOfWork.BeginTransaction();
				var personRepo = _unitOfWork.GetRepository<Person>();
				var person = new Person
				{
					Name = request.Name,
					Email = request.Email
				};

				await personRepo.InsertAsync(person);
				await _unitOfWork.SaveAsync();

				//_unitOfWork.CommitTransaction();

				return person;
			}
			catch (Exception ex)
			{
				_unitOfWork.RollBack();
				throw;
			}
			finally
			{
				_unitOfWork.Dispose();
			}
		}
	}
}
