using MediatR;
using N5.Application.DTOs;

namespace N5.Application.Queries
{
    public record GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>;
}
