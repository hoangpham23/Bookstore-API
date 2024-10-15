using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.LanguageCmd;

public class CreateLanguage : IRequest<LanguageDTO>
{
    public required string LanguageName { get; set; }
    public required string LanguageCode { get; set; }
}
