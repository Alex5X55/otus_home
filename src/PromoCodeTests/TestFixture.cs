using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeTests
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// Выполняется перед запуском тестов
        /// </summary>
        public TestFixture()
        {
            var builder = new ConfigurationBuilder();
            var configuration = builder.Build();
            var serviceCollection = DependencyInjection.AddServices((IServiceCollection)null, configuration, DIResource.Testing_IOC);
            var serviceProvider = serviceCollection
                .BuildServiceProvider();
            ServiceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

    }
}
