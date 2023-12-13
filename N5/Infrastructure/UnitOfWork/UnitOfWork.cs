using N5.Infrastructure.Repositories;

namespace N5.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IPermissionRepository _permissionRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPermissionRepository PermissionRepository
        {
            get { return _permissionRepository ??= new PermissionRepository(_context); }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }

}
