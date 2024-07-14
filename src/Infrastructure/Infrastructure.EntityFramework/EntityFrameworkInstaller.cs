using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString) 
        {
            services.AddDbContext<DatabaseContext>(OptionsBuilder
                => OptionsBuilder
                .UseLazyLoadingProxies() // lazy loading
            //.UseNpgsql(connectionString));
                    .UseSqlite(connectionString));
            //.UseSqlServer(connectionString));
            return services;
        }
    }
}
