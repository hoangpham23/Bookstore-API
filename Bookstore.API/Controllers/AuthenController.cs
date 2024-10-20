using Bookstore.Core.Base;
using BookStore.Application.Commands.AuthenCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            try
            {
                var loginDTO = await _mediator.Send(request);
                return Ok(BaseResponse<LoginDTO>.OkResponse(loginDTO, "Login Successfully"));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Authen Controller");
            }
        }
    }
}
