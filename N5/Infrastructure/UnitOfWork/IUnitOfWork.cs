using N5.Infrastructure.Repositories;

namespace N5.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPermissionRepository PermissionRepository { get; }
        Task SaveChanges();
    }
}
