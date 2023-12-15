using Microsoft.EntityFrameworkCore;
using N5.Domain;
using N5.Interfaces;

namespace N5.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;
        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdatePermission(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public async Task CreatePermission(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetPermissionById(int id)
        {
            return await _context.Permissions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PermissionType?> GetPermissionTypeById(int id)
        {
            return await _context.PermissionTypes.FirstOrDefaultAsync(y => y.Id == id);
        }
    }
}
