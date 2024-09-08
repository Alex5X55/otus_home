using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Otus.Teaching.PromoCodeFactory.WebHost;
using Otus.Teaching.PromoCodeFactory.WebHost.Controllers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using PromoCodeWebApiTest;
using Services.Abstractions;
using Services.Contracts.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeTests
{
    public class ControllerTestFixture : IDisposable
    {
        public Mock<IPartnerService> PartnerServiceMock { get; set; }
        public Mock<IMapper> PartnerMapperMock  { get; set; }
        public Mock<ILogger<PartnersController>> PartnerLoggerMock { get; set; }
        public Guid Id { get; set; }
        public CancellationToken CancellationToken { get; set; } 
        public SetPartnerPromoCodeLimitRequest SetPartnerPromoCodeLimitRequest { get; set; }
        public SetPartnerPromoCodeLimitRequestDto SetPartnerPromoCodeLimitRequestDto { get; set; }
        public PartnersController PartnersController { get; set; }
        /// <summary>
        /// Выполняется перед запуском тестов
        /// </summary>
        public ControllerTestFixture()
        {
            PartnerServiceMock = new Mock<IPartnerService>();
            PartnerMapperMock = new Mock<IMapper>();
            PartnerLoggerMock = new Mock<ILogger<PartnersController>>();
            PartnersController = new PartnersController(PartnerServiceMock.Object,
                PartnerMapperMock.Object,
                PartnerLoggerMock.Object);

            Id = Guid.NewGuid();
            CancellationToken = new CancellationTokenSource().Token;
            SetPartnerPromoCodeLimitRequest = ControllerTestData.InitSetPartnerPromoCodeLimitRequest();
            SetPartnerPromoCodeLimitRequestDto = ControllerTestData.InitSetPartnerPromoCodeLimitRequestDto();


            // var builder = new ConfigurationBuilder();
            // var configuration = builder.Build();
            // var serviceCollection = DependencyInjection.AddServices((IServiceCollection)null, configuration, DIResource.Testing_IOC);
            //  var serviceProvider = serviceCollection
            //      .BuildServiceProvider();
            //  ServiceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

    }
}
