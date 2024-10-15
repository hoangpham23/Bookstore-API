using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.AuthorCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.AuthorCmdHandler;

public class CreateAuthorHandler : IRequestHandler<CreateAuthor, AuthorDTO>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<AuthorDTO> Handle(CreateAuthor request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var authorRepo = _unitOfWork.GetRepository<Author>();
            var author = _mapper.Map<Author>(request);

            await authorRepo.InsertAsync(author);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();
            
            return _mapper.Map<AuthorDTO>(author);
        }
        catch (System.Exception)
        {
            _unitOfWork.RollBack();
            throw;
        }
        finally{
            _unitOfWork.Dispose();
        }
    }
}
