using ClinicalTrials.Domain.ClinicalTrials;
using Microsoft.EntityFrameworkCore;

namespace ClinicalTrials.Infrastructure.Database
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ClinicalTrial> ClinicalTrials { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}