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
        private readonly ILogger<PermissionsController> _logger;
        public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<PermissionDto>> GetAll()
        {
            _logger.LogInformation("Ejecutando GetAllPermissionsQuery...");
            var permissions = await _mediator.Send(new GetAllPermissionsQuery());
            _logger.LogInformation("Se ejecutó con éxito GetAllPermissionsQuery.");
            return permissions;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePermissionCommand command)
        {
            _logger.LogInformation("Ejecutando UpdatePermissionCommand...");
            if (id != command.Id)
            {
                return BadRequest();
            }

            var permission = await _mediator.Send(command);
            if (permission==null)
            {
                return NotFound();
            }
            _logger.LogInformation("Se ejecutó con éxito UpdatePermissionCommand.");
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Create(CreatePermissionCommand command)
        {
            _logger.LogInformation("Ejecutando CreatePermissionCommand...");
            var permission = await _mediator.Send(command);
            _logger.LogInformation("Se ejecutó con éxito CreatePermissionCommand...");
            return permission;
        }
    }
}
