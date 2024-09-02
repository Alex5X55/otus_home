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
using AutoMapper;
using Otus.Teaching.PromoCodeFactory.WebHost.Mapping;
using Microsoft.EntityFrameworkCore;


namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration configuration, string serviceType)
        {
            if (serviceCollection == null)
            {
                serviceCollection = new ServiceCollection();
            }

            if(serviceType.Equals(DIResource.Working_IOC))
            {
                var applicationSettings = configuration.Get<ApplicationSettings>();
                serviceCollection
                    .AddSingleton(applicationSettings)
                    .ConfigureContext(applicationSettings.ConnectionStringSQLite);
            }

           // if (serviceType.Equals(DIResource.Testing_IOC))
           // {
           //     serviceCollection
           //         .ConfigureInMemoryContext();
           //         
           // }


            serviceCollection
                .AddSingleton((IConfigurationRoot)configuration)
                .SetAutomapper()
            //////.AddSingleton(configuration)
            // serviceCollection.ConfigureContext(applicationSettings.ConnectionStringSQLite);
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

        private static IServiceCollection SetAutomapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return serviceCollection;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleMappingProfile>();
                cfg.AddProfile<EmploeeMappingProfile>();
                cfg.AddProfile<CustomerMappingProfile>();
                cfg.AddProfile<PreferenceMappingProfile>();
                cfg.AddProfile<PromoCodeMappingProfile>();
                cfg.AddProfile<CustomerPreferenceMappingProfile>();
                cfg.AddProfile<CustomerPromoCodeMappingProfile>();
                cfg.AddProfile<PartnerMappingProfile>();
                cfg.AddProfile<PartnerPromoCodeLimitMappingProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }

        public static IServiceCollection ConfigureInMemoryContext(this IServiceCollection serviceCollection)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkProxies()
                .BuildServiceProvider();
            serviceCollection.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb", builder => { });
                options.UseInternalServiceProvider(serviceProvider);
                options.UseLazyLoadingProxies();
            });
            serviceCollection.AddTransient<DbContext, DatabaseContext>();
            return serviceCollection;
        }

    }
}
