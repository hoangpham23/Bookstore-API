using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.PublisherQr;

public record SearchPublisher(int Index, string? PublisherName) : IRequest<BasePaginatedList<PublisherDTO>>;
