﻿namespace BookStore.Application.DTOs
{
    public class BookDTO
    {
        public string BookId { get; set; } = null!;
        public string? Title { get; set; }
        public List<string>? AuthorNames { get; set; }
        public string? Isbn13 { get; set; }
        public string? LanguageName { get; set; } // Add this property for the language name
        public string? PublisherName { get; set; }
        public int? NumPages { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public required decimal Price { get; set; }
    }

}
