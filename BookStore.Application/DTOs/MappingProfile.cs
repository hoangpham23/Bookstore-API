using AutoMapper;
using Bookstore.Domain.Entites;
using BookStore.Application.Commands;
using BookStore.Application.Commands.AddressCmd;
using BookStore.Application.Commands.AuthorCmd;
using BookStore.Application.Commands.CustomerCmd;
using BookStore.Application.Commands.LanguageCmd;


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
                            (src => src.Publisher != null ? src.Publisher.PublisherName : "Unknow Publisher"))
                .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src =>
                src.Authors != null ? src.Authors.Select(a => a.AuthorName).ToList() : new List<string>()));

            CreateMap<CreateBook, Book>();

            CreateMap<Publisher, PublisherDTO>()
                    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));

            CreateMap<BookLanguage, LanguageDTO>()
                    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Books.Select(b => b.Title).ToList()));

            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => new BookTitleDTO
                {
                    Title = b.Title,
                    BookId = b.BookId
                }).ToList()));

            CreateMap<UpdateBook, Book>();

            CreateMap<CreateAuthor, Author>();
            CreateMap<UpdateAuthor, Author>();

            CreateMap<CreateLanguage, BookLanguage>();
            CreateMap<UpdateLanguage, BookLanguage>();

            CreateMap<Address, AddressSummaryDTO>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom
                        (src => src.Country != null ? src.Country.CountryName : "Unknown Country"));

            CreateMap<CustomerAddress, AddressSummaryDTO>()
                .IncludeMembers(src => src.Address);

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.CustomerAddresses));

            CreateMap<Customer, CustomerDTO>()
           .ForMember(dest => dest.Addresses, opt => opt.Ignore());

            CreateMap<CreateCustomer, Customer>();
            CreateMap<CreateCustomer, Address>();

            CreateMap<UpdateCustomer, Customer>();
            CreateMap<UpdateCustomer, Address>();


            CreateMap<Customer, CustomerSummaryDTO>();

            CreateMap<CustomerAddress, CustomerSummaryDTO>().IncludeMembers(src => src.Customer);
            CreateMap<UpdateAddress, Address>();

            CreateMap<Customer, OrderCustomerDTO>();
            CreateMap<CustOrder, CustOrderDTO>();
            CreateMap<ShippingMethod, ShippingDTO>();
            CreateMap<OrderLine, OrderBooksDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : "Unknow Title"))
                .ForMember(dest => dest.Isbn13, opt => opt.MapFrom(src => src.Book != null ? src.Book.Isbn13 : "Unknow ISBN13"));
        }
    }
}
