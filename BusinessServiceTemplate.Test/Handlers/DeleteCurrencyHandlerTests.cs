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
    public class DeleteCurrencyHandlerTests
    {
        private List<SC_Currency> _currencyStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public DeleteCurrencyHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CurrencyDataToDomainMapper>();
            });

            _currencyStore = StoreFactory.CurrencyStore;
        }

        [Fact]
        public async Task WhenClientTriggeringCurrencyDelete_ThenSpecificCurrencyDeleted_ReturnTheDeletedCurrency() 
        {
            // Mock
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();

            // Setup
            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p)));

            scCurrencyRepositoryMock.Setup(m => m.Delete(It.IsAny<SC_Currency>()))
                .Returns((SC_Currency p) => 
                {
                    _currencyStore.Remove(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScCurrencyRepository).Returns(scCurrencyRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var deleteHandler = new DeleteCurrencyHandler(unitOfWorkMock.Object, autoMapper);

            var request = new DeleteCurrencyRequest
            {
                Id = 5
            };

            // Assert
            var existedObject = _currencyStore.Find(x => x.Id == request.Id);
            existedObject.Should().NotBeNull();

            // Sut
            var result = await deleteHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _currencyStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().BeNull();

            scCurrencyRepositoryMock.Verify(m => m.Delete(It.IsAny<SC_Currency>()), Times.Once);
        }
    }
}
