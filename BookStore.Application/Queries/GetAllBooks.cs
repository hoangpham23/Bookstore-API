using System;
using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using MediatR;

namespace BookStore.Application.Queries;

public class GetAllBooks : IRequest<BasePaginatedList<BookDTO>>
{
    private int _index;
    public GetAllBooks(int index)
    {
        if (index < 0) _index = 1;
        else _index = index;
    }
    public int Index
    {
        get { return _index; }
        set
        {
            if (_index < 0) _index =  1;
            else _index = value;
        }
    }

}
