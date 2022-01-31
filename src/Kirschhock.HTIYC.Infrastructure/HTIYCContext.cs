using Kirschhock.HTIYC.Domain;

using Microsoft.EntityFrameworkCore;

namespace Kirschhock.HTIYC.Infrastructure
{
    public class HTIYCContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Fact> Facts { get; set; }

        public HTIYCContext(DbContextOptions<HTIYCContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HTIYCContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
