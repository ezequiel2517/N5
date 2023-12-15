using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Application.Queries;
using N5.Controllers;
using N5.Domain;
using N5.Interfaces;

namespace TestApi
{
    [TestClass]
    public class PermissionsControllerTest
    {
        [TestMethod]
        public async Task GetAll_ShouldReturnOkWithPermissionsDto()
        {
            //Dependencias del controlador
            var mediatorMock = new Mock<IMediator>();
            var kafkaProducerMock = new Mock<IKafkaProducerService>();
            var loggerMock = new Mock<ILogger<PermissionsController>>();

            //Datos de prueba
            var permissions = new List<PermissionDto>
            {
                new PermissionDto
                {
                    Id = 1,
                    EmployeeForename = "John",
                    EmployeeSurname = "Doe",
                    PermissionType = 1,
                    PermissionDate = DateTime.Now
                },
                new PermissionDto
                {
                    Id = 2,
                    EmployeeForename = "Jane",
                    EmployeeSurname = "Smith",
                    PermissionType = 2,
                    PermissionDate = DateTime.Now.AddDays(1)
                }
            };
            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllPermissionsQuery>(), default))
                .ReturnsAsync(permissions);

            //Inicializando contralador
            var controller = new PermissionsController(mediatorMock.Object, loggerMock.Object, kafkaProducerMock.Object);

            //Ejecutando metodo
            var actionResult = await controller.GetAll();

            //Validando tipo del metodo
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<PermissionDto>>));

            //Validando codigo de respuesta
            var objectResult = actionResult.Result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, objectResult?.StatusCode);

            //Validando permissions de respuesta
            var returnedPermissions = objectResult?.Value as IEnumerable<PermissionDto>;
            Assert.IsNotNull(returnedPermissions);
            Assert.AreEqual(permissions.Count(), returnedPermissions.Count());

            //Validando kafka
            kafkaProducerMock.Verify(x => x.Produce("methods", "GET"), Times.Once);
        }

        [TestMethod]
        public async Task Create_ShouldReturnOkWithPermissionDto()
        {
            //Dependencias del controlador
            var mediatorMock = new Mock<IMediator>();
            var kafkaProducerMock = new Mock<IKafkaProducerService>();
            var loggerMock = new Mock<ILogger<PermissionsController>>();
            
            //Datos de prueba
            var permissionDto = new PermissionDto
            {
                Id = 3,
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionType = 1,
                PermissionDate = DateTime.Now
            };
            var command = new CreatePermissionCommand(permissionDto.EmployeeForename, 
                permissionDto.EmployeeSurname, permissionDto.PermissionType, permissionDto.PermissionDate);
            mediatorMock.Setup(x => x.Send(It.IsAny<CreatePermissionCommand>(), default))
                .ReturnsAsync(permissionDto);

            //Inicializando contralador
            var controller = new PermissionsController(mediatorMock.Object, loggerMock.Object, kafkaProducerMock.Object);

            //Ejecutando metodo
            var actionResult = await controller.Create(command);

            //Validando codigo de respuesta
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            //Validando tipo de la respuesta
            var returnedPermission = okResult.Value as PermissionDto;
            Assert.IsNotNull(returnedPermission);

            //Validando kafka
            kafkaProducerMock.Verify(x => x.Produce("methods", "POST"), Times.Once);
        }

        [TestMethod]
        public async Task Update_ShouldReturnOk()
        {
            //Dependencias del controlador
            var mediatorMock = new Mock<IMediator>();
            var kafkaProducerMock = new Mock<IKafkaProducerService>();
            var loggerMock = new Mock<ILogger<PermissionsController>>();

            //Datos de prueba
            var permissionDto = new PermissionDto
            {
                Id = 1,
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionType = 1,
                PermissionDate = DateTime.Now
            };
            var command = new UpdatePermissionCommand(permissionDto.Id, permissionDto.EmployeeForename,
                permissionDto.EmployeeSurname, permissionDto.PermissionType, permissionDto.PermissionDate);
            mediatorMock.Setup(x => x.Send(It.IsAny<UpdatePermissionCommand>(), default))
                .ReturnsAsync(permissionDto);

            //Inicializando contralador
            var controller = new PermissionsController(mediatorMock.Object, loggerMock.Object, kafkaProducerMock.Object);

            //Ejecutando metodo
            var actionResult = await controller.Update(permissionDto.Id, command);

            //Validando codigo de respuesta
            var okResult = actionResult as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult?.StatusCode);

            //Validando kafka
            kafkaProducerMock.Verify(x => x.Produce("methods", "PUT"), Times.Once);
        }

    }
}