using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.AuthorQr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAuthor([FromQuery] SearchAuthor request)
        {
            try
            {
                var authors = await _mediator.Send(request);
                if (authors == null || authors.TotalItems == 0)
                {
                    return NotFound(BaseResponse<string>.NotFoundResponse("Author list is empty"));
                }
                return Ok(BaseResponse<BasePaginatedList<AuthorDTO>>.OkResponse(authors, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at search author controller: " + ex.Message));
            }

        }
    }
}
