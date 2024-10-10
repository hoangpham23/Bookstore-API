using AutoMapper;
using Bookstore.Infrastructure;
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
            (src => src.Language != null ? src.Language.LanguageName : "Unknown Language"));

            CreateMap<CreateBook, Book>();
        }
    }
}
