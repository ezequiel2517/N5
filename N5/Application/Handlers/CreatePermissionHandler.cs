using MediatR;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Domain;
using N5.Infrastructure;
using N5.Interfaces;

namespace N5.Application.Handlers
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, PermissionDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService _elasticsearchService;

        public CreatePermissionHandler(IUnitOfWork unitOfWork, IElasticSearchService elasticsearchService)
        {
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
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
            await _elasticsearchService.InsertDocument("permissions", permission);

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
