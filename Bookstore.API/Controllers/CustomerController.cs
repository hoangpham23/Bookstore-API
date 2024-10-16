using Bookstore.Core.Base;
using BookStore.Application.Commands.CustomerCmd;
using BookStore.Application.DTOs;
using BookStore.Application.Queries.CustomerQr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            try
            {
                var request = new GetCustomerById { CustomerId = id };
                var customerDTO = await _mediator.Send(request);
                return Ok(BaseResponse<CustomerDTO>.OkResponse(customerDTO, "Customer's information"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(BaseResponse<string>.NotFoundResponse("Error at the Language Controller: " + ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at the Customer controller: " + ex.Message));
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomer([FromQuery] int index, [FromQuery] SearchCustomer request)
        {
            try
            {
                request.Index = index;
                var customerDTO = await _mediator.Send(request);
                if (customerDTO.TotalItems == 0) return NotFound(BaseResponse<string>.NotFoundResponse("The customer list is empty"));
                return Ok(BaseResponse<BasePaginatedList<CustomerDTO>>.OkResponse(customerDTO, "Customer's information"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at the Customer controller: " + ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer request)
        {
            try
            {
                var customerDTO = await _mediator.Send(request);
                return Ok(BaseResponse<CustomerDTO>.OkResponse(customerDTO, "Create customer successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, BaseResponse<string>.InternalErrorResponse("Error at the Customer Controller: " + ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] UpdateCustomer request)
        {
            try
            {
                request.CustomerId = id;
                var customerDTO = await _mediator.Send(request);
                return Ok(BaseResponse<CustomerDTO>.OkResponse(customerDTO, "Update successfully"));
            }catch(Exception ex)
            {
                return HandleException(ex, "Customer Controller");
            }
        }

    }
}
