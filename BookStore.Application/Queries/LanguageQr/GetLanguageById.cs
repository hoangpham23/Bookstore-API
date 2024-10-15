using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.LanguageQr;

public class GetLanguageById : IRequest<LanguageDTO>
{
    public required string LanguageId { get; set; }
}
