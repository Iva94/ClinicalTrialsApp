using ClinicalTrials.Domain.ClinicalTrials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicalTrials.Infrastructure.Database.Configurations
{
    internal class ClinicalTrialConfiguration : IEntityTypeConfiguration<ClinicalTrial>
    {
        public void Configure(EntityTypeBuilder<ClinicalTrial> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.StartDate).HasColumnType("Date").IsRequired();
            builder.Property(p => p.EndDate).HasColumnType("Date");
            builder.Property(p => p.Status).IsRequired();
        }
    }
}
