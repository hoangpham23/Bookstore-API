using Bookstore.Core.Base;
using Bookstore.Infrastructure;
using BookStore.Application.Commands;
using BookStore.Application.DTOs;
using BookStore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBook createBook)
        {
            try
            {
                var book = await _mediator.Send(createBook);
                return Ok(BaseResponse<BookDTO>.OkResponse(book, "create successfully"));
            }
            catch (Exception ex)
            {

                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error: " + ex.Message));
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int index)
        {
            try
            {
                var books = await _mediator.Send(new GetAllBooks(index));

                if (books == null || books.TotalItems == 0)
                {
                    return NotFound(BaseResponse<string>.NotFoundResponse("Book list is empty"));
                }

                return Ok(BaseResponse<BasePaginatedList<BookDTO>>.OkResponse(books, null));

            }
            catch (Exception ex)
            {

                return StatusCode(500, BaseResponse<string>.InternalErrorResponse(ex.Message));
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBook([FromQuery] int index, [FromQuery] string? languageName = null, [FromQuery] string? title = null){
            try
            {
                var books = await _mediator.Send(new SearchBook{
                    Index = index,
                    LanguageName = languageName,
                    Title = title
                });

                 if (books == null || books.TotalItems == 0)
                {
                    return NotFound(BaseResponse<string>.NotFoundResponse("Book list is empty"));
                }

                return Ok(BaseResponse<BasePaginatedList<BookDTO>>.OkResponse(books, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error: " + ex.Message));
            }
        }
    }
}
