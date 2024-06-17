using CleanTemplate.Core.Interfaces.Infrastructure;
using CleanTemplate.Core.Interfaces.Services;
using CleanTemplate.Core.Services;
using CleanTemplate.Infrastructure.Context;
using CleanTemplate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Presentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBooksRepository, BooksRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksService, BooksService>();

            return services;
        }

        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            AddRepositories(services);
            AddServices(services);

            return services;
        }
    }
}
