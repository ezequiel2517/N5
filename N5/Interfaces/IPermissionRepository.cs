using N5.Domain;

namespace N5.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllPermissions();
        Task CreatePermission(Permission permission);
        Task UpdatePermission(Permission permission);
        Task<Permission?> GetPermissionById(int id);
        Task<PermissionType?> GetPermissionTypeById(int id);
    }
}
