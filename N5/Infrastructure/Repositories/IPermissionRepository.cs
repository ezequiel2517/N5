using N5.Domain;

namespace N5.Infrastructure.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAll();
        Task Create(Permission permission);
        Task Update(Permission permission);
        Task<Permission?> GetById(int id);
    }
}
