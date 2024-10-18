using Bookstore.Core.Base;
using BookStore.Application.Commands.ShippingCmd;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.ShippingQr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : BaseController
    {
        private readonly IMediator _mediator;

        public ShippingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippingMethod()
        {
            try
            {
                var shippingDTOs = await _mediator.Send(new GetAllShippingMethod());
                return Ok(BaseResponse<List<ShippingDTO>>.OkResponse(shippingDTOs, "Shipping method list"));
            }
            catch (Exception ex)
            {
                
                return HandleException(ex, "Shipping Controller");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatShippingMethod([FromBody] CreateShippingMethod request)
        {
            try
            {
                var shippingDTO = await _mediator.Send(request);
                return Ok(BaseResponse<ShippingDTO>.OkResponse(shippingDTO, "Create Shipping Method Successfully"));
            }
            catch (Exception ex)
            {
                
                return HandleException(ex, "Shipping Controller");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> CreatShippingMethod(string id, [FromBody] UpdateShippingMethod request)
        {
            try
            {
                request.MethodId = id;
                var shippingDTO = await _mediator.Send(request);
                return Ok(BaseResponse<ShippingDTO>.OkResponse(shippingDTO, "Update Shipping Method Successfully"));
            }
            catch (Exception ex)
            {
                
                return HandleException(ex, "Shipping Controller");
            }
        }
    }
}
