using System;
using AutoMapper;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.PublisherQr;
using MediatR;

namespace BookStore.Application.QueryHandlers.PublisherHandler;

public class GetPublisherByIdHandler : IRequestHandler<GetPublisherById, PublisherDTO>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPublisherByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PublisherDTO> Handle(GetPublisherById request, CancellationToken cancellationToken)
    {
        var publisherRepo = _unitOfWork.GetRepository<Publisher>();
        var publisher = await publisherRepo.GetByIdAsync(request.PublisherId);
        if (publisher == null) throw new KeyNotFoundException("The publisher doesn't exist");
        return _mapper.Map<PublisherDTO>(publisher);
    }
}
