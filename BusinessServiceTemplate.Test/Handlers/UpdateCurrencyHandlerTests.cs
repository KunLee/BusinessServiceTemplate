using AutoMapper;
using BusinessServiceTemplate.Core.Handlers;
using BusinessServiceTemplate.Core.Mappers;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Test.Common;
using FluentAssertions;
using Moq;

namespace BusinessServiceTemplate.Test.Handlers
{
    public class UpdateCurrencyHandlerTests
    {
        private List<SC_Currency> _currencyStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public UpdateCurrencyHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CurrencyDataToDomainMapper>();
            });

            _currencyStore = StoreFactory.CurrencyStore;
        }

        [Fact]
        public async Task WhenClientTriggeringCurrencyUpdate_ThenSpecificCurrencyUpdated_ReturnTheUpdatedCurrency() 
        {
            // Mock
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();

            // Setup
            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p))).Verifiable();

            scCurrencyRepositoryMock.Setup(m => m.Update(It.IsAny<SC_Currency>()))
                .Returns((SC_Currency p) => 
                {
                    var currencyToUpdate = _currencyStore.Find(x => x.Id == p.Id);
                    Assert.NotNull(currencyToUpdate);
                    currencyToUpdate.Name = p.Name;
                    currencyToUpdate.Country = p.Country;
                    currencyToUpdate.Symbol = p.Symbol;
                    currencyToUpdate.Shortcode = p.Shortcode;
                    currencyToUpdate.Active = p.Active;
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScCurrencyRepository).Returns(scCurrencyRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var updateHandler = new UpdateCurrencyHandler(unitOfWorkMock.Object, autoMapper);

            var request = new UpdateCurrencyRequest
            {
                Id = 1,
                Name = "Update Currency",
                Shortcode = "Update Shortcode",
                Country = "Update Country",
                Symbol = "Update Symbol",
                Active= false
            };

            var oldObject = _currencyStore.Find(x => x.Id == request.Id);
            var oldName = oldObject?.Name;
            var oldShortcode = oldObject?.Shortcode;
            var oldCountry = oldObject?.Country;
            var oldSymbol = oldObject?.Symbol;
            var oldActive = oldObject?.Active;

            var result = await updateHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _currencyStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().NotBeNull();
            verifiedObject?.Name.Should().Be(request.Name);
            verifiedObject?.Country.Should().Be(request.Country);
            verifiedObject?.Shortcode.Should().Be(request.Shortcode);
            verifiedObject?.Symbol.Should().Be(request.Symbol);
            verifiedObject?.Active.Should().Be(request.Active);

            verifiedObject?.Name.Should().NotBe(oldName);
            verifiedObject?.Country.Should().NotBe(oldCountry);
            verifiedObject?.Shortcode.Should().NotBe(oldShortcode);
            verifiedObject?.Symbol.Should().NotBe(oldSymbol);
            verifiedObject?.Active.Should().NotBe(oldActive);
            // Verify
            scCurrencyRepositoryMock.Verify(m => m.Update(It.IsAny<SC_Currency>()), Times.Once);
            scCurrencyRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
        }
    }
}
