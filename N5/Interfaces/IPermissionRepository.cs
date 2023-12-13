using N5.Domain;

namespace N5.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAll();
        Task Create(Permission permission);
        Task Update(Permission permission);
        Task<Permission?> GetById(int id);
    }
}
