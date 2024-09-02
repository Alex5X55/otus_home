using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using Otus.Teaching.PromoCodeFactory.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeTests
{
    public class TestFixture_InMemory : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IServiceCollection ServiceCollection { get; set; }
        /// <summary>
        /// Выполняется перед запуском тестов
        /// </summary>
        public TestFixture_InMemory()
        {
            var builder = new ConfigurationBuilder();
            var configuration = builder.Build();
            ServiceCollection = DependencyInjection.AddServices((IServiceCollection)null, configuration, DIResource.Testing_IOC);
            ServiceProvider = GetServiceProvider();
           // ServiceProvider = serviceProvider;
        }

        private IServiceProvider GetServiceProvider()
        {
            var serviceProvider = ServiceCollection
                .ConfigureInMemoryContext()
                .BuildServiceProvider();
            
            SeedData.SeedFakeData(serviceProvider);
            return serviceProvider;
        }

        public void Dispose()
        {
        }

    }
}
