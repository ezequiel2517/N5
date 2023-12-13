using MediatR;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Domain;
using N5.Infrastructure;
using N5.Infrastructure.UnitOfWork;

namespace N5.Application.Handlers
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, PermissionDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PermissionDto?> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                EmployeeForename = request.EmployeeForename,
                EmployeeSurname = request.EmployeeSurname,
                PermissionType = request.PermissionType,
                PermissionDate = request.PermissionDate
            };

            await _unitOfWork.PermissionRepository.Create(permission);
            await _unitOfWork.SaveChanges();

            return new PermissionDto
            {
                Id = permission.Id,
                EmployeeForename = permission.EmployeeForename,
                EmployeeSurname = permission.EmployeeSurname,
                PermissionType = permission.PermissionType,
                PermissionDate = permission.PermissionDate,
            };
        }
    }
}
