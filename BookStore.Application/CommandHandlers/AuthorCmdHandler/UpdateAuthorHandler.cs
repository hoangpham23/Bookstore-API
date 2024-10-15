using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.AuthorCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.Application.CommandHandlers.AuthorCmdHandler;

public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, AuthorDTO>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<AuthorDTO> Handle(UpdateAuthor request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var authorRepo = _unitOfWork.GetRepository<Author>();
            var author = await authorRepo.FindByConditionAsync(a => a.AuthorId == request.AuthorId);
            if (author == null) throw new KeyNotFoundException("Author doesn't exist");
            _mapper.Map(request, author);
            
            await authorRepo.UpdateAsync(author);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<AuthorDTO>(author);
        }
        catch (System.Exception)
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
