using MediatR;
using N5.Application.DTOs;

namespace N5.Application.Commands
{
    public record CreatePermissionCommand(string EmployeeForename, string EmployeeSurname, 
        int PermissionType, DateTime PermissionDate) : IRequest<PermissionDto>;
}
