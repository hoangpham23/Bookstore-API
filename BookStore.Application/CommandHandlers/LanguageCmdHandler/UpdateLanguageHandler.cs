using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands.LanguageCmd;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.CommandHandlers.LanguageCmdHandler;

public class UpdateLanguageHandler : IRequestHandler<UpdateLanguage, LanguageDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLanguageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LanguageDTO> Handle(UpdateLanguage request, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var languageRepo = _unitOfWork.GetRepository<BookLanguage>();
            var language = await languageRepo.FindByConditionAsync(l => l.LanguageId == request.LanguageId);
            if (language == null) throw new KeyNotFoundException("Book Language doesn't exist");

            _mapper.Map(request, language);
            await languageRepo.UpdateAsync(language);
            await _unitOfWork.SaveChangeAsync();
            _unitOfWork.CommitTransaction();

            return _mapper.Map<LanguageDTO>(language);
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            throw new Exception("Error while updating the book language entity: " + ex.Message);
        }
        finally{
            _unitOfWork.Dispose();
        }
    }
}
