﻿using AutoMapper;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands;
using BookStore.Application.Queries.PublisherQr;


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
               // if it deosn't include order in it will return 0 
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.OrderLines
                       .Where(ol => ol.BookId == src.BookId)
                       .Select(ol => ol.Price)
                       .FirstOrDefault() ?? 0))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom
                            (src => src.Publisher != null ? src.Publisher.PublisherName : "Unknow Publisher"));
            CreateMap<CreateBook, Book>();
            CreateMap<Publisher, PublisherDTO>()
                    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));
        }
    }
}
