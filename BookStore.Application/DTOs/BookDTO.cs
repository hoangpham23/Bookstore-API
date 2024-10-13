namespace BookStore.Application.DTOs
{
    public class BookDTO
    {
        public string BookId { get; set; } = null!;
        public string? Title { get; set; }
        public string? Isbn13 { get; set; }
        public string? LanguageName { get; set; } // Add this property for the language name
        public int? NumPages { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public Decimal? Price { get; set; }
    }

}
