using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Repositories.Abstractions;
using Moq;
using Services.Implementations;
using AutoMapper;
using MassTransit.JobService;
using Serilog;
using Services.Contracts.Partner;
using System.Xml.Linq;
using Services.Implementations.Exceptions.Partner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.WebHost;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace PromoCodeTests
{
    public class SetPartnerPromoCodeLimitAsyncTests_IOC: IDisposable
    {
        private IPartnerService _partnerService;
        
        private Mock<IRepository<Partner>> _partnerRepositoryMock
            = new Mock<IRepository<Partner>> ();

        private Mock<IRepository<PartnerPromoCodeLimit>> _partnerPromoCodeLimitRepositoryMock
            = new Mock<IRepository<PartnerPromoCodeLimit>> ();



        public SetPartnerPromoCodeLimitAsyncTests_IOC()
        {
            var builder = new ConfigurationBuilder();
            var configuration = builder.Build();
            var serviceCollection = DependencyInjection.AddServices((IServiceCollection)null, configuration, DIResource.Testing_IOC);
            var serviceProvider = serviceCollection
                .BuildServiceProvider();

            _partnerService = new PartnerService(
                  serviceProvider.GetService<IMapper>(),
                  _partnerRepositoryMock.Object,
                  _partnerPromoCodeLimitRepositoryMock.Object
                );
        }


        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_IOC_Returns_Success_For_Valid_Data()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;

            var partnerLimits = new List<PartnerPromoCodeLimit>()
            {
                new PartnerPromoCodeLimitBuilder()
                .WithId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435011"))
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithCreateDate(DateTime.Now.AddDays(-3))
                .WithCancelDate(DateTime.Now)
                .WithEndDate(DateTime.Now.AddMonths(3))
                .WithLimit(10)
                .Build(),
                new PartnerPromoCodeLimitBuilder()
                .WithId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435033"))
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithCreateDate(DateTime.Now)
                .WithCancelDate(DateTime.MaxValue)
                .WithEndDate(DateTime.Now.AddMonths(3))
                .WithLimit(5)
                .Build(),
            };

            var partner = new PartnerBuilder().WithId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithName("Спортмастер")
                .WithNumberIssuedPromoCodes(1)
                .WithIsActive(true)
                .WithPartnerLimits(partnerLimits)
                .Build();

            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithEndDate(DateTime.Now)
                .WithLimit(15)
                .Build();

            var newLimit = new PartnerPromoCodeLimit()
            {
                Id = Guid.NewGuid(),
                Limit = setPartnerPromoCodeLimitRequestDto.Limit,
                Partner = partner,
                PartnerId = partner.Id,
                CreateDate = DateTime.Now,
                CancelDate = DateTime.MaxValue,
                EndDate = setPartnerPromoCodeLimitRequestDto.EndDate
            };

            var partnerPromoCodeLimitResponseDto = new PartnerPromoCodeLimitResponseDtoBuilder()
               .WithId(newLimit.Id)
               .WithPartnerId(partner.Id)
               .WithCreateDate(newLimit.CreateDate)
               .WithCancelDate(newLimit.CancelDate ?? DateTime.MaxValue)
               .WithEndDate(newLimit.EndDate)
               .WithLimit(newLimit.Limit)
               .Build();

            _partnerRepositoryMock.Setup(m =>
                m.GetByIdAsync(It.IsAny<Guid>(), cancellationToken)).ReturnsAsync(partner);

            _partnerPromoCodeLimitRepositoryMock.Setup(m =>
                m.AddAsync(newLimit, cancellationToken)).ReturnsAsync(newLimit);

            _partnerRepositoryMock.Setup(m =>
                m.UpdateAsync(partner, cancellationToken)).ReturnsAsync(partner);

            _partnerPromoCodeLimitRepositoryMock.Setup(m =>
                m.GetByIdAsync(newLimit.Id, cancellationToken)).ReturnsAsync(newLimit);

            await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken);

            //Act
            //Assert
            Assert.Equal(setPartnerPromoCodeLimitRequestDto.PartnerId, partnerPromoCodeLimitResponseDto.PartnerId);
            Assert.Equal(setPartnerPromoCodeLimitRequestDto.Limit, partnerPromoCodeLimitResponseDto.Limit);
            Assert.Equal(setPartnerPromoCodeLimitRequestDto.EndDate, partnerPromoCodeLimitResponseDto.EndDate);
        }


        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_IOC_Pantner_Not_Found_Successfull()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithEndDate(DateTime.Now)
                .WithLimit(15)
                .Build();
            
            _partnerRepositoryMock.Setup(m =>
                m.GetByIdAsync(setPartnerPromoCodeLimitRequestDto.PartnerId, cancellationToken)).ReturnsAsync((Partner)null);


            //Act
            //Assert
            await Assert.ThrowsAsync<PartnerNotFoundException>(async () => await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken));

        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_IOC_Partner_Is_Bloked_Successfull()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithEndDate(DateTime.Now)
                .WithLimit(15)
                .Build();
            var partner = new PartnerBuilder().WithId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435003"))
                .WithName("Спортмастер")
                .WithNumberIssuedPromoCodes(1)
                .WithIsActive(false)
                .Build();

            _partnerRepositoryMock.Setup(m =>
                m.GetByIdAsync(It.IsAny<Guid>(), cancellationToken)).ReturnsAsync(partner);

            //Act
            //Assert
            await Assert.ThrowsAsync<PartnerIsBlokedException>(async () => await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken));

        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_IOC_Partner_Limit_Is_Empty_Successfull()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithEndDate(DateTime.Now)
                .WithLimit(0)
                .Build();
            var partner = new PartnerBuilder().WithId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435003"))
                .WithName("Спортмастер")
                .WithNumberIssuedPromoCodes(1)
                .WithIsActive(true)
                .Build();

            _partnerRepositoryMock.Setup(m =>
                m.GetByIdAsync(It.IsAny<Guid>(), cancellationToken)).ReturnsAsync(partner);

            //Act
            //Assert
            await Assert.ThrowsAsync<PartnerLimitIsEmptyException>(async () => await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken));

        }

        public void Dispose()
        {
        }


    }
}
