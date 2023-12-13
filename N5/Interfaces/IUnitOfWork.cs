namespace N5.Interfaces
{
    public interface IUnitOfWork
    {
        IPermissionRepository PermissionRepository { get; }
        Task SaveChanges();
    }
}
