using Bookstore.Core.Base;
using Bookstore.Infrastructure;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllBooks();
                var books = await _mediator.Send(query);

                if (books == null || books.Count == 0){
                    return NotFound(BaseResponse<string>.NotFoundResponse("Book list is empty"));
                }

                return Ok(BaseResponse<IList<BookDTO>>.OkResponse(books, null));

            }
            catch (Exception ex)
            {
                
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse(ex.Message));
            }
        }
    }
}
