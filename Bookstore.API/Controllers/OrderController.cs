using Bookstore.Core.Base;
using BookStore.Application.Commands.OrderCmd;
using BookStore.Application.DTOs;
using BookStore.Application.QueryHandlers.CustOrderQrHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController

    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var request = new GetCustOrderById { OrderId = id };
                var custOrderDTO = await _mediator.Send(request);
                return Ok(BaseResponse<CustOrderDTO>.OkResponse(custOrderDTO, "Order information"));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Order Controller");
            }

        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder request)
        {
            try
            {
                var custOrderDTO = await _mediator.Send(request);
                return Ok(BaseResponse<CustOrderDTO>.OkResponse(custOrderDTO, "Order information"));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Order Controller");
            }
        }
    }
}
