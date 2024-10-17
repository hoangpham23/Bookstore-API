namespace BookStore.Application.DTOs;

public class CustOrderDTO
{
    public CustOrderDTO()
    {
        
    }
    public string? OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    // in this DTO will include the Address information
    public  OrderCustomerDTO? Customer { get; set; }
    public  ShippingDTO? Shipping { get; set; }
    public List<OrderBooksDTO> OrderBooks { get; set; } = new List<OrderBooksDTO>();
}
