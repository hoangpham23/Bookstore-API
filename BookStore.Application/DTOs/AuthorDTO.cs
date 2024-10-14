namespace BookStore.Application.DTOs;

public class AuthorDTO
{
    public string AuthorId { get; set; } = null!;
    public string? AuthorName { get; set; }
    public List<string>? BookTitle { get; set; }
}
