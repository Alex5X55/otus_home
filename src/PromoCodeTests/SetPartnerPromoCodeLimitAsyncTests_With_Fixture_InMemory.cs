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
    public class SetPartnerPromoCodeLimitAsyncTests_With_Fixture_InMemory: IClassFixture<TestFixture_InMemory>
    {
        private IPartnerService _partnerService;
        
        public SetPartnerPromoCodeLimitAsyncTests_With_Fixture_InMemory(TestFixture_InMemory testFixture)
        {
            var serviceProvider = testFixture.ServiceProvider;
            _partnerService = serviceProvider.GetService<IPartnerService>();
        }


        [Fact]
        public async Task SetPartnerPromoCodeLimitAsyncTests_With_Fixture_InMemory_Returns_Success_For_Valid_Data()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;

            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithEndDate(DateTime.Now.AddDays(7))
                .WithLimit(15)
                .Build();


            var partnerPromoCodeLimitResponseDto = await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken);

            //Act
            //Assert
            Assert.Equal(setPartnerPromoCodeLimitRequestDto.PartnerId, partnerPromoCodeLimitResponseDto.PartnerId);
            Assert.Equal(setPartnerPromoCodeLimitRequestDto.Limit, partnerPromoCodeLimitResponseDto.Limit);
            Assert.Equal(setPartnerPromoCodeLimitRequestDto.EndDate, partnerPromoCodeLimitResponseDto.EndDate);
        }


        [Fact]
        public async Task SetPartnerPromoCodeLimitAsyncTests_With_Fixture_InMemory_Pantner_Not_Found_Successfull()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435000"))
                .WithEndDate(DateTime.Now.AddDays(7))
                .WithLimit(15)
                .Build();
            
            //Act
            //Assert
            await Assert.ThrowsAsync<PartnerNotFoundException>(async () => await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken));

        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsyncTests_With_Fixture_InMemory_Partner_Is_Bloked_Successfull()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435002"))
                .WithEndDate(DateTime.Now)
                .WithLimit(15)
                .Build();

            //Act
            //Assert
            await Assert.ThrowsAsync<PartnerIsBlokedException>(async () => await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken));

        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsyncTests_With_Fixture_InMemory_Partner_Limit_Is_Empty_Successfull()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.Parse("F766E2BF-340A-46EA-BFF3-F1700B435001"))
                .WithEndDate(DateTime.Now)
                .WithLimit(0)
                .Build();

            //Act
            //Assert
            await Assert.ThrowsAsync<PartnerLimitIsEmptyException>(async () => await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken));

        }
    }
}
