using Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.WebHost.Settings;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using Infrastructure.EntityFramework;
using Umbraco.Core.Persistence;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using Umbraco.Core.Composing.CompositionExtensions;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();
            serviceCollection.AddSingleton(applicationSettings)
                    .AddSingleton((IConfigurationRoot)configuration)
                    .ConfigureContext(applicationSettings.ConnectionStringSQLite)
                    .InstallRepositories()
                    .InstallServices();
            return serviceCollection;
        }

        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IEmployeeService, EmploeeService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<IPreferenceService, PreferenceService>()
            .AddScoped<IPromoCodeService, PromoCodeService>()
            .AddScoped<IPartnerService, PartnerService>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                 .AddScoped<IRepository<Role>, RoleRepository>()
                 .AddScoped<IRepository<Employee>, EmployeeRepository>()
                 .AddScoped<IRepository<Customer>, CustomerRepository>()
                 .AddScoped<IRepository<Preference>, PreferenceRepository>()
                 .AddScoped<IRepository<CustomerPreference>, CustomerPreferenceRepository>()
                 .AddScoped<IRepository<PromoCode>, PromoCodeRepository>()
                 .AddScoped<IRepository<CustomerPromoCode>, CustomerPromoCodeRepository>()
                 .AddScoped<IRepository<Partner>, PartnerRepository>()
                 .AddScoped<IRepository<PartnerPromoCodeLimit>, PartnerPromoCodeLimitRepository>();

            //.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            return serviceCollection;
        }
    }
}
