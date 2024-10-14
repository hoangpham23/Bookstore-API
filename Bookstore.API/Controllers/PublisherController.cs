using Bookstore.Core.Base;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.PublisherQr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublisherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchPblisher([FromQuery] int index , [FromQuery] string? publisherName)
        {
            try
            {
                var request = new SearchPublisher(index, publisherName);
                var publishers = await _mediator.Send(request);
                
                if (publishers == null || publishers.TotalItems == 0){
                    return NotFound(BaseResponse<string>.NotFoundResponse("Publisher list is empty"));
                }

                return Ok(BaseResponse<BasePaginatedList<PublisherDTO>>.OkResponse(publishers, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at the publisher controller:  " + ex.Message));
            }
        }
    }
}
