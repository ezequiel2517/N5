using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Application.Queries;
using N5.Interfaces;
using System.Collections;

namespace N5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PermissionsController> _logger;
        private readonly IKafkaProducerService _kafkaProducerService;
        public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger, 
            IKafkaProducerService kafkaProducerService)
        {
            _mediator = mediator;
            _logger = logger;
            _kafkaProducerService = kafkaProducerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Ejecutando GetAllPermissionsQuery...");
                await _kafkaProducerService.Produce("methods", "GET");
                var permissions = await _mediator.Send(new GetAllPermissionsQuery());             
                _logger.LogInformation("Se ejecutó con éxito GetAllPermissionsQuery.");
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en GetAllPermissionsQuery: {ex.Message}");
                return NotFound(new { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePermissionCommand command)
        {
            _logger.LogInformation("Ejecutando UpdatePermissionCommand...");
            await _kafkaProducerService.Produce("methods", "PUT");
            if (id != command.Id)
            {
                return BadRequest(new {Message = "El id de ruta no coincide con el del body."});
            }

            try
            {
                var permission = await _mediator.Send(command);
                if (permission == null)
                {
                    return NotFound(new { Message = "El id de permission no existe." });
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en UpdatePermissionCommand: {ex.Message}");
                return NotFound(new { ex.Message });
            }

            _logger.LogInformation("Se ejecutó con éxito UpdatePermissionCommand.");
            return Ok(new {Message = $"Permission de id ({id}) actualizado con éxito."});
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Create(CreatePermissionCommand command)
        {
            try
            {
                _logger.LogInformation("Ejecutando CreatePermissionCommand...");
                await _kafkaProducerService.Produce("methods", "POST");
                var permission = await _mediator.Send(command);
                _logger.LogInformation("Se ejecutó con éxito CreatePermissionCommand...");
                return Ok(permission);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error en CreatePermissionCommand: {ex.Message}");
                return NotFound(new { ex.Message });
            }
        }
    }
}
