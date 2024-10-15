namespace BookStore.Application.DTOs;

public class AuthorDTO
{
    public string AuthorId { get; set; } = null!;
    public string? AuthorName { get; set; }
    public List<BookTitleDTO>? Books { get; set; }
}
