using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.Infrastructure.Persistence 
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }

}