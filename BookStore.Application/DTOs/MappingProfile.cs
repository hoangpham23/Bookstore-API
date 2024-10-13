using AutoMapper;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands;


namespace BookStore.Application.DTOs
{
    public class MappingProfile : Profile
    {
        // left class is the resource, right class is the destination
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom
                            (src => src.Language != null ? src.Language.LanguageName : "Unknown Language"))
               
               // this run 
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.OrderLines
                       .Where(ol => ol.BookId == src.BookId)
                       .Select(ol => ol.Price)
                       .FirstOrDefault() ?? 0
                       ));
            CreateMap<CreateBook, Book>();
        }
    }
}
