using Bookstore.Core.Base;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.PublisherQr;

public class SearchPublisher : IRequest<BasePaginatedList<PublisherDTO>>
{
    public SearchPublisher(int index, string? publisherName)
    {
        Index = index;
        PublisherName = publisherName;
    }

    public int Index {get; set;}
    public string? PublisherName { get; set; }
    
}
