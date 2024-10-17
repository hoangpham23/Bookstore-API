using Bookstore.Core.Base;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.OrderHistoryQr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderHistory([FromQuery] GetAllOrderHistory request)
        {
            try
            {
                var orderHistoryDTO = await _mediator.Send(request);
                if (orderHistoryDTO.TotalItems == 0) return NotFound(BaseResponse<string>.NotFoundResponse("You don't have any orders"));
                return Ok(BaseResponse<BasePaginatedList<OrderHistoryDTO>>.OkResponse(orderHistoryDTO, "Order history information"));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Order History Controller");
            }
        }
    }
}
