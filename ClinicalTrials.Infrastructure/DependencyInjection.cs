using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Infrastructure.Database;
using ClinicalTrials.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicalTrials.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IClinicalTrialRepository, ClinicalTrialRepository>();

            return services;
        }
    }
}
