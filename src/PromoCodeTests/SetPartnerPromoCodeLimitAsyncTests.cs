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


namespace PromoCodeTests
{
    public class SetPartnerPromoCodeLimitAsyncTests
    {
        private IPartnerService _partnerService;
        
        private Mock<IRepository<Partner>> _partnerRepositoryMock
            = new Mock<IRepository<Partner>> ();

        private Mock<IRepository<PartnerPromoCodeLimit>> _partnerPromoCodeLimitRepositoryMock
            = new Mock<IRepository<PartnerPromoCodeLimit>> ();

        private Mock<IMapper> _mapperMock
            = new Mock<IMapper>();

        public SetPartnerPromoCodeLimitAsyncTests()
        {
            _partnerService = new PartnerService(
                  _mapperMock.Object,
                  _partnerRepositoryMock.Object,
                  _partnerPromoCodeLimitRepositoryMock.Object
                );
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimit_Pantner_Not_Found_Successfull()
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
    }
}
