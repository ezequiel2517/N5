using Microsoft.EntityFrameworkCore;
using N5.Domain;

namespace N5.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
