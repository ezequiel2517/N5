using MediatR;
using N5.Application.Commands;
using N5.Application.DTOs;
using N5.Infrastructure;
using N5.Interfaces;
using N5.Services;

namespace N5.Application.Handlers
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService _elasticsearchService;

        public UpdatePermissionHandler(IUnitOfWork unitOfWork, IElasticSearchService elasticsearchService)
        {
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
        }

        public async Task<PermissionDto?> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken) {
            var permission = await _unitOfWork.PermissionRepository.GetPermissionById(request.Id);
            if (permission == null) 
                return null;
            permission.Id = request.Id;
            permission.EmployeeForename = request.EmployeeForename;
            permission.PermissionType = request.PermissionType;
            permission.PermissionDate = request.PermissionDate;

            var idPermissionType = await _unitOfWork.PermissionRepository.GetPermissionTypeById(permission.PermissionType);
            if (idPermissionType == null)
            {
                throw new Exception($"PermissionType ({permission.PermissionType}) no existe.");
            }

            await _unitOfWork.PermissionRepository.UpdatePermission(permission);
            await _unitOfWork.SaveChanges();
            await _elasticsearchService.InsertDocument("permissions", permission);
            
            return new PermissionDto { 
                Id = permission.Id, 
                EmployeeForename = permission.EmployeeForename, 
                EmployeeSurname = permission.EmployeeSurname,
                PermissionType = permission.PermissionType,
                PermissionDate = permission.PermissionDate,
            };
        }
    }
}
