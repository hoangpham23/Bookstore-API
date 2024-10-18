using Bookstore.Core.Base;
using BookStore.Application.Commands.OrderStatusCmd;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.OrderStatusQr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/order-status")]
    [ApiController]
    public class OrderStatusController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatus()
        {
            try
            {
                var orderStatusDTOs = await _mediator.Send(new GetAllOrderStatus());
                return Ok(BaseResponse<List<OrderStatusDTO>>.OkResponse(orderStatusDTOs, "Order Status list"));
            }
            catch (Exception ex)
            {
                
                return HandleException(ex, "Order Status Controller");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrderStatus([FromBody] CreateOrderStatus request)
        {
            try
            {
                var orderStatusDTO = await _mediator.Send(request);
                return Ok(BaseResponse<OrderStatusDTO>.OkResponse(orderStatusDTO, "Create Order Status Successfully"));
            }
            catch (Exception ex)
            {
                
                return HandleException(ex, "Order Status Controller");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatus request)
        {
            try
            {
                request.StatusId = id;
                var orderStatusDTO = await _mediator.Send(request);
                return Ok(BaseResponse<OrderStatusDTO>.OkResponse(orderStatusDTO, "Update Order Status Successfully"));
            }
            catch (Exception ex)
            {
                
                return HandleException(ex, "Order Status Controller");
            }
        }


    }
}
