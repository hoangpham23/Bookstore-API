using System;

namespace BookStore.Application.DTOs;

public class BookTitleDTO
{
    public required string BookId { get; set; }
    public required string Title { get; set; }
}
