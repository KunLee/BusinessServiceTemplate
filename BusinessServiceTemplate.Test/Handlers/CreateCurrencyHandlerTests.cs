using AutoMapper;
using BusinessServiceTemplate.Core.Handlers;
using BusinessServiceTemplate.Core.Mappers;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using BusinessServiceTemplate.Shared.Exceptions;
using BusinessServiceTemplate.Test.Common;
using FluentAssertions;
using Moq;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace BusinessServiceTemplate.Test.Handlers
{
    public class CreateCurrencyHandlerTests
    {
        private List<SC_Currency> _currencyStore;
        private IdGenerator _idGenerator;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public CreateCurrencyHandlerTests()
        {
            _idGenerator = new IdGenerator(10);

            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CurrencyDataToDomainMapper>();
            });

            _currencyStore = StoreFactory.CurrencyStore;
        }

        [Fact]
        public async Task WhenClientTriggeringCurrencyCreate_ThenNewCurrencyAdded_ReturnTheCreatedCurrency() 
        {
            // Mock
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();

            // Setup
            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p)));

            scCurrencyRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Currency>()))
                .Returns((SC_Currency p) => 
                {
                    p.Id = _idGenerator.Next();
                    _currencyStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScCurrencyRepository).Returns(scCurrencyRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateCurrencyHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateCurrencyRequest
            {
                Name = "New Currency",
                Country = "New Country",
                Shortcode = "New Shortcode",
                Symbol = "New Symbol",
                Active = false
            };

            var result = await createHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _currencyStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().NotBeNull();
            verifiedObject?.Id.Should().Be(_idGenerator.Last());
            verifiedObject?.Name.Should().Be(request.Name);
            verifiedObject?.Country.Should().Be(request.Country);
            verifiedObject?.Shortcode.Should().Be(request.Shortcode);
            verifiedObject?.Symbol.Should().Be(request.Symbol);
            verifiedObject?.Active.Should().Be(request.Active);
            scCurrencyRepositoryMock.Verify(m => m.Create(It.IsAny<SC_Currency>()), Times.Once);
        }

        [Fact]
        public async Task WhenClientTriggeringCurrencyCreate_ThenDuplicateCurrencyFound_ExceptionThrown()
        {
            // Mock
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();

            // Setup
            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p)));

            scCurrencyRepositoryMock.Setup(m => m.Any(It.IsAny<Expression<Func<SC_Currency, bool>>>()))
                .Returns((Expression<Func<SC_Currency, bool>> p) => Task.FromResult(_currencyStore.Any(p.Compile()))).Verifiable();

            scCurrencyRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Currency>()))
                .Returns((SC_Currency p) =>
                {
                    p.Id = _idGenerator.Next();
                    _currencyStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScCurrencyRepository).Returns(scCurrencyRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateCurrencyHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateCurrencyRequest
            {
                Name = "Currency Duplicate",
                Country = "Country Duplicate",
                Shortcode = "Shortcode Duplicate",
                Symbol = "Symbol Duplicate",
                Active = true
            };

            // Sut
            Func<Task> act = () => createHandler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                        .Where(e => e.Message.StartsWith(ConstantStrings.DUPLICATE_REQUEST_DATA));

            scCurrencyRepositoryMock.Verify(m => m.Any(It.IsAny<Expression<Func<SC_Currency, bool>>>()), Times.Once);
            scCurrencyRepositoryMock.Verify(m => m.Create(It.IsAny<SC_Currency>()), Times.Never);
        }
    }
}
