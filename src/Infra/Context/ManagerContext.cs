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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SLER24T;Initial Catalog=UserManager;Integrated Security=SSPI;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}