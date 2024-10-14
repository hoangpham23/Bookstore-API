namespace BookStore.Application.DTOs;

public class LanguageDTO{
    public string LanguageId { get; set; } = null!;
    public string? LanguageName { get; set; } 
    public string? LanguageCode { get; set; }
    public List<string>? BookTitle { get; set; }
}