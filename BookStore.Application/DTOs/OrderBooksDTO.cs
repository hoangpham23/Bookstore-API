namespace BookStore.Application.DTOs;

public class OrderBooksDTO
{
    public required string BookId { get; set; }
    public required string Title { get; set; }
    public required string Isbn13 { get; set; }
    public decimal Price { get; set; }
}
