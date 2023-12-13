using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Application.Queries;

namespace N5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PermissionDto>> GetAll()
        {
            return await _mediator.Send(new GetAllPermissionsQuery());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePermissionCommand command)
        {
            Console.Write(command);
            if (id != command.Id)
            {
                return BadRequest();
            }

            var permission = await _mediator.Send(command);
            if (permission==null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Create(CreatePermissionCommand command)
        {
            var permission = await _mediator.Send(command);
            return permission;
        }
    }
}
