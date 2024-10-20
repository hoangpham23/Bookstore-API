using System.Text.Json.Serialization;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Commands.AuthorCmd;

public class UpdateAuthor : IRequest<AuthorDTO>
{
    [JsonIgnore]
    public string? AuthorId { get; set; }
    public required string AuthorName { get; set; }
}
