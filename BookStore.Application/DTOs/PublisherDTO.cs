namespace BookStore.Application.DTOs;

public class PublisherDTO
{
    public string PublisherId { get; set; } = null!;
    public string? PublisherName { get; set;}
    public List<string>? BookTitle { get; set; }
}
