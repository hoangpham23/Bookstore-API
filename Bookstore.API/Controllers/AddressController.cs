using Bookstore.Core.Base;
using BookStore.Application.Commands.AddressCmd;
using BookStore.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(string id)
        {
            try
            {
                var addressDTO = await _mediator.Send(new GetAddressById(id));
                return Ok(BaseResponse<AddressDTO>.OkResponse(addressDTO, "Address's information"));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Address Controller");
            }
        }
    

        [HttpGet]
        public async Task<IActionResult> SearchAddress([FromQuery] SearchAddress request)
        {
            try
            {
                var addresDTO = await _mediator.Send(request);
                return Ok(BaseResponse<BasePaginatedList<AddressDTO>>.OkResponse(addresDTO, "Address information"));
            }
            catch (Exception ex)
            {
                return HandleException(ex , "Address Controller");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(string id, [FromBody] UpdateAddress request)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest(BaseResponse<string>.BadRequestResponse("The addressId is required"));
                request.AddressId = id;
                var addresDTO = await _mediator.Send(request);
                return Ok(BaseResponse<AddressDTO>.OkResponse(addresDTO, "Address information"));
            }
            catch (Exception ex)
            {
                return HandleException(ex , "Address Controller");
            }
        }

    }
}
