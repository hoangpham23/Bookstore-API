using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.LanguageCmd;

public class UpdateLanguage : IRequest<LanguageDTO>
{
    public string? LanguageId { get; set; }
    public required string LanguageName { get; set; }
    public required string LanguageCode { get; set; }
}
