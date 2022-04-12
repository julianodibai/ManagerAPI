using Domain.Entities;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class ManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ManagerContext() { }
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}