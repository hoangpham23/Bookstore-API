using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.LanguageCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.LanguageCmdHandler;

public class CreateLanguageHandler : IRequestHandler<CreateLanguage, LanguageDTO>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLanguageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<LanguageDTO> Handle(CreateLanguage request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var languageRepo = _unitOfWork.GetRepository<BookLanguage>();
            var language = _mapper.Map<BookLanguage>(request);

            await languageRepo.InsertAsync(language);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<LanguageDTO>(language);
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("Error while saving the Book Language entity: " + ex.Message);
        }
        finally{
            _unitOfWork.Dispose();
        }
    }
}
