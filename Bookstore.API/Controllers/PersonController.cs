using Application.PersonCmd.Commands;
using Bookstore.Core.Base;
using Bookstore.Domain.Entities;
using BookStore.Application.PersonCmd.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bookstore.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

		[HttpPost]
		public async Task<IResult> CreatePerson([FromBody] CreatePerson command)
		{
			var result = await _mediator.Send(command);
			return Results.Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _mediator.Send(new GetAllPersons());
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAll(int id)
		{
			try
			{
				var person = await _mediator.Send(new GetPersonById { Id = id });
				return Ok(BaseResponse<Person>.OkResponse(person, null));
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(BaseResponse<string>.NotFoundResponse(ex.Message));
			}
			catch (Exception ex)
			{
				return StatusCode(500, BaseResponse<string>.InternalErrorResponse(ex.Message)); 		
			}
		}
    }
}
