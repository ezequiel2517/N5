using MediatR;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Application.Queries;
using N5.Interfaces;

namespace N5.Application.Handlers
{
    public class GetAllPermisionsHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPermisionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.PermissionRepository.GetAllPermissions();
            return permissions.Select(permission => new PermissionDto
            {
                Id = permission.Id,
                EmployeeForename = permission.EmployeeForename,
                EmployeeSurname = permission.EmployeeSurname,
                PermissionDate = permission.PermissionDate,
                PermissionType = permission.PermissionType,
            });
        }
    }
}
