using System;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries.PublisherQr;

public class GetPublisherById : IRequest<PublisherDTO>
{
    public required string PublisherId { get; set; }
}
