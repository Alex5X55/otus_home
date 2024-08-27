using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using Otus.Teaching.PromoCodeFactory.WebHost.Controllers;
using Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using PromoCodeTests;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Contracts.Partner;
using Services.Implementations.Exceptions.Partner;
using Otus.Teaching.PromoCodeFactory.WebHost.PromoCodeStatusCodeResults;
using System.Globalization;

namespace PromoCodeWebApiTest
{
    public class PartnerControllerTests
    {
        private PartnersController _partnersController;

        private Mock<IPartnerService> _partnerServiceMock = new Mock<IPartnerService>();
        private Mock<IMapper> _partnerMapperMock = new Mock<IMapper>();
        private Mock<ILogger<PartnersController>> _partnerLoggerMock = new Mock<ILogger<PartnersController>>();

        private Guid id;
        private SetPartnerPromoCodeLimitRequest setPartnerPromoCodeLimitRequest;
        private SetPartnerPromoCodeLimitRequestDto setPartnerPromoCodeLimitRequestDto;
        private CancellationToken cancellationToken;

        public PartnerControllerTests()
        {
            _partnersController = new PartnersController(_partnerServiceMock.Object,
                _partnerMapperMock.Object,
                _partnerLoggerMock.Object);

            id = Guid.NewGuid();
            setPartnerPromoCodeLimitRequest = new SetPartnerPromoCodeLimitRequestBuilder()
                .WithEndDate(DateTime.Now.AddMonths(1))
                .WithLimit(18)
                .Build();
            setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDtoBuilder()
                .WithPartnerId(Guid.NewGuid())
                .WithEndDate(DateTime.Now.AddDays(5))
                .WithLimit(44)
                .Build();
            cancellationToken = new CancellationTokenSource().Token;
        }


        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_Returns_Success_For_Valid_Data_200()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var partnerPromoCodeLimitResponseDto = new PartnerPromoCodeLimitResponseDtoBuilder()
                .WithId(Guid.NewGuid())
                .WithPartnerId(id)
                .WithCreateDate(DateTime.Now)
                .WithCancelDate(DateTime.MaxValue)
                .WithEndDate(setPartnerPromoCodeLimitRequest.EndDate)
                .WithLimit(setPartnerPromoCodeLimitRequest.Limit)
                .Build();
            var partnerPromoCodeLimitResponse = new PartnerPromoCodeLimitResponseBuilder()
                .WithId(partnerPromoCodeLimitResponseDto.Id)
                .WithPartnerId(partnerPromoCodeLimitResponseDto.PartnerId)
                .WithCreateDate(partnerPromoCodeLimitResponseDto.CreateDate)
                .WithCancelDate(partnerPromoCodeLimitResponseDto.CancelDate ?? DateTime.Now)
                .WithEndDate(partnerPromoCodeLimitResponseDto.EndDate)
                .WithLimit(setPartnerPromoCodeLimitRequest.Limit)
                .Build();

            _partnerServiceMock.Setup(m =>
               m.SetPartnerPromoCodeLimitAsync(It.IsAny<SetPartnerPromoCodeLimitRequestDto>(), cancellationToken)).ReturnsAsync(partnerPromoCodeLimitResponseDto);

            _partnerMapperMock.Setup(m =>
               m.Map<PartnerPromoCodeLimitResponse>(partnerPromoCodeLimitResponseDto)).Returns(partnerPromoCodeLimitResponse);


            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(id, setPartnerPromoCodeLimitRequest, cancellationToken);

            //Assert

            var okRequestResult = Assert.IsType<OkObjectResult>(result);
            var okObjectResult = Assert.IsType<PartnerPromoCodeLimitResponse>(okRequestResult.Value);
            Assert.Equal(id, okObjectResult.PartnerId);
            Assert.Equal(setPartnerPromoCodeLimitRequest.EndDate.ToString(CultureInfo.CurrentCulture), okObjectResult.EndDate);
            Assert.Equal(setPartnerPromoCodeLimitRequest.Limit, okObjectResult.Limit);

            //var PartnerLimitIsEmptyObjectResult = Assert.IsType<PartnerLimitIsEmptyObjectResult>(result);
            //Assert.Equal(Resources.PartnerControllerTest_PartnerLimitIsEmpty, PartnerLimitIsEmptyObjectResult.Value);


            //var NotFoundObjectResult = Assert.IsType<>(NotFoundResult.Value);

            //var okRequestResult = Assert.IsType<OkObjectResult>(result);
            //var okObjectResult = Assert.IsType<WeatherForecast>(okRequestResult.Value);
            //Assert.Equal(id, okObjectResult.Id);
        }


        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_Partner_Not_Found_Return_NotFound_404()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            _partnerServiceMock.Setup(m =>
               m.SetPartnerPromoCodeLimitAsync(It.IsAny<SetPartnerPromoCodeLimitRequestDto>(), cancellationToken)).ThrowsAsync(new PartnerNotFoundException());

            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(id, setPartnerPromoCodeLimitRequest, cancellationToken);
            //Assert

            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(Resources.PartnerControllerTest_PartnerNotFound, NotFoundResult.Value);
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_Partner_Is_Bloked_Return_BadRequest_400()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            _partnerServiceMock.Setup(m =>
               m.SetPartnerPromoCodeLimitAsync(It.IsAny<SetPartnerPromoCodeLimitRequestDto>(), cancellationToken)).ThrowsAsync(new PartnerIsBlokedException());

            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(id, setPartnerPromoCodeLimitRequest, cancellationToken);
            //Assert

            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(Resources.PartnerControllerTest_PartnerIsBloked, BadRequestResult.Value);

        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_Partner_Limit_Is_Empty_Return_PartnerLimitIsEmpty_715()
        {
            //Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            _partnerServiceMock.Setup(m =>
               m.SetPartnerPromoCodeLimitAsync(It.IsAny<SetPartnerPromoCodeLimitRequestDto>(), cancellationToken)).ThrowsAsync(new PartnerLimitIsEmptyException());

            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(id, setPartnerPromoCodeLimitRequest, cancellationToken);
            //Assert

            var PartnerLimitIsEmptyObjectResult = Assert.IsType<PartnerLimitIsEmptyObjectResult>(result);
            Assert.Equal(Resources.PartnerControllerTest_PartnerLimitIsEmpty, PartnerLimitIsEmptyObjectResult.Value);


            //var NotFoundObjectResult = Assert.IsType<>(NotFoundResult.Value);


            //var okRequestResult = Assert.IsType<OkObjectResult>(result);
            //var okObjectResult = Assert.IsType<WeatherForecast>(okRequestResult.Value);
            //Assert.Equal(id, okObjectResult.Id);
        }


    }
}
