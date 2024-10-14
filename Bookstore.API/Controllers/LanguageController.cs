using Bookstore.Core.Base;
using Bookstore.Domain.Entites;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.LanguageQr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchLanguage([FromQuery] SearchLangugae request)
        {
            try
            {
                var bookLanguages = await _mediator.Send(request);

                if (bookLanguages == null || bookLanguages.TotalItems == 0)
                {
                    return NotFound(BaseResponse<string>.NotFoundResponse("The language list is empty"));
                }

                return Ok(BaseResponse<BasePaginatedList<LanguageDTO>>.OkResponse(bookLanguages, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at seach controller: " + ex.Message));
            }
        }
    }
}
