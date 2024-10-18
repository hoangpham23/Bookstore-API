namespace BookStore.Application.DTOs;

public class OrderStatusDTO
{
    public int StatusId { get; set; }

    public required string StatusValue { get; set; }
}
