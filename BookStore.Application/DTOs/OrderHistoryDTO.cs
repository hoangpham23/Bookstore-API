namespace BookStore.Application.DTOs;

public class OrderHistoryDTO
{
    public string OrderId { get; set; }
    public OrderBooksDTO OrderBooks { get; set; }
    public decimal TotalPrice { get; set; }
    public string OrderStatus { get; set; }
    public DateTime StatusDate { get; set; }

    public OrderHistoryDTO()
    {
        OrderId = string.Empty; 
        OrderBooks = new OrderBooksDTO(); 
        TotalPrice = 0.0m;
        OrderStatus = string.Empty; 
        StatusDate = new DateTime();
    }
}
