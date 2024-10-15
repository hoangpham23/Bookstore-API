using Bookstore.Core.Base;
using BookStore.Application.Commands.AuthorCmd;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
            try
            {
                var request = new GetAuthorById { AuthorId = id };
                var authorDTO = await _mediator.Send(request);
                return Ok(BaseResponse<AuthorDTO>.OkResponse(authorDTO, "Author's Information"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(BaseResponse<string>.NotFoundResponse("Error at GetAuthorById: " + ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at Author Controller: " + ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatAuthor([FromBody] CreateAuthor request)
        {
            try
            {
                var authorDTO = await _mediator.Send(request);
                return Ok(BaseResponse<AuthorDTO>.OkResponse(authorDTO, "Author's Information"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(BaseResponse<string>.NotFoundResponse("Error at GetAuthorById: " + ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at Author Controller: " + ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(string id, [FromBody] UpdateAuthor request)
        {
            try
            {
                request.AuthorId = id;
                var authorDTO = await _mediator.Send(request);
                return Ok(BaseResponse<AuthorDTO>.OkResponse(authorDTO, "Author's Information"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(BaseResponse<string>.NotFoundResponse("Error at GetAuthorById: " + ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at Author Controller: " + ex.Message));
            }
        }
    }
}
