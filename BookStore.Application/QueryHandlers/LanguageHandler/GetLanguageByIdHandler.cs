using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.LanguageQr;
using MediatR;

namespace BookStore.Application.QueryHandlers.LanguageHandler;

public class GetLanguageByIdHandler : IRequestHandler<GetLanguageById, LanguageDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLanguageByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LanguageDTO> Handle(GetLanguageById request, CancellationToken cancellationToken)
    {
        var languageRepo = _unitOfWork.GetRepository<BookLanguage>();
        var language = await languageRepo.GetByIdAsync(request.LanguageId);
        if (language == null) throw new KeyNotFoundException("The language doens't exist");

        return _mapper.Map<LanguageDTO>(language);
    }
}
