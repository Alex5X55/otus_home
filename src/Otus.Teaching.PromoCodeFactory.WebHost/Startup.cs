using AutoMapper;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using Otus.Teaching.PromoCodeFactory.WebHost.Mapping;
using Services.Implementations.Mapping;
using static MassTransit.MessageHeaders;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Services.AddSingleton(FakeDataFactory.Employees());
            //SetAutomapper(services);
            services.AddServices(Configuration, DIResource.Working_IOC);





            services.AddControllers();

            //services.AddScoped(typeof(IRepository<Employee>), (x) => 
            //  new InMemoryRepository<Employee>(FakeDataFactory.Employees));

            //services.AddSingleton(typeof(IRepository<Employee>), (x) =>
            //    new InMemoryRepository<Employee>(FakeDataFactory.Employees));

            //services.AddScoped(typeof(IRepository<Role>), (x) => 
            //  new InMemoryRepository<Role>(FakeDataFactory.Roles));

            //services.AddSingleton(typeof(IRepository<Role>), (x) =>
            //   new InMemoryRepository<Role>(FakeDataFactory.Roles));


            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocExpansion = "list";
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       /* private static IServiceCollection SetAutomapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return services;
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
       */
    }
}