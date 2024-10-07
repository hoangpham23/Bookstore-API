using System;
using Bookstore.Infrastructure;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries;

public class GetAllBooks : IRequest<IList<BookDTO>>
{
    
}
