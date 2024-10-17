namespace BookStore.Application.DTOs;

public class OrderBooksDTO
{
    public string BookId { get; set; }
    public string Title { get; set; }
    public string Isbn13 { get; set; }
    public decimal Price { get; set; }

    public OrderBooksDTO()
    {
        BookId = string.Empty;
        Title = string.Empty;
        Isbn13 = string.Empty;
        Price = 0.0m;
    }

}
