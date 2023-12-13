using Microsoft.EntityFrameworkCore;
using N5.Domain;

namespace N5.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;
        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Update(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public async Task Create(Permission permission)
        {
            _context.Permissions.Add(permission);
        }

        public async Task<IEnumerable<Permission>> GetAll()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetById(int id)
        {
            return await _context.Permissions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
