using MediatR;
using N5.Application.DTOs;

namespace N5.Application.Commands
{
    public record UpdatePermissionCommand(int Id, string EmployeeForename, string EmployeeSurname,
        int PermissionType, DateTime PermissionDate) : IRequest<PermissionDto>;
}
