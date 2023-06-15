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
    public class UpdatePanelHandlerTests
    {
        private List<SC_Panel> _panelStore;
        private List<SC_Test> _testStore;
        private List<SC_TestSelection> _testSelectionStore;
        private List<SC_Currency> _currencyStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public UpdatePanelHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PanelDataToDomainMapper>();
                cfg.AddProfile<TestDataToDomainMapper>();
                cfg.AddProfile<CurrencyDataToDomainMapper>();
            });

            _panelStore = StoreFactory.PanelStore;
            _testStore = StoreFactory.TestStore;
            _testSelectionStore = StoreFactory.TestSelectionStore;
            _currencyStore = StoreFactory.CurrencyStore;
        }

        [Fact]
        public async Task WhenClientTriggeringPanelUpdate_ThenPanelPropertiesUpdated_ReturnTheUpdatedPanel() 
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();
            var scTestRepositoryMock = new Mock<IScTestRepository>();
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p)));

            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p))).Verifiable();

            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p))).Verifiable();

            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p))).Verifiable();

            scPanelRepositoryMock.Setup(m => m.Update(It.IsAny<SC_Panel>()))
                .Returns((SC_Panel p) => 
                {
                    var panelToUpdate = _panelStore.Find(x => x.Id == p.Id);
                    Assert.NotNull(panelToUpdate);
                    panelToUpdate.Name = p.Name; 
                    panelToUpdate.Description = p.Description;
                    panelToUpdate.Price = p.Price;
                    panelToUpdate.PriceVisibility = p.PriceVisibility;
                    panelToUpdate.Visibility= p.Visibility;
                    panelToUpdate.DescriptionVisibility = p.DescriptionVisibility;
                    panelToUpdate.TestSelectionId = p.TestSelectionId;
                    panelToUpdate.CurrencyId = p.CurrencyId;
                    panelToUpdate.Tests = p.Tests;
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScCurrencyRepository).Returns(scCurrencyRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var updateHandler = new UpdatePanelHandler(unitOfWorkMock.Object, autoMapper);

            var request = new UpdatePanelRequest
            {
                Id = 1,
                Name = "Update Panel",
                Description = "Update Panel Desc",
                DescriptionVisibility = true,
                Price = 101.01m,
                PriceVisibility = false,
                TestIds = new List<int> { 2 },
                TestSelectionId = 2,
                CurrencyId = 3,
                Visibility = false
            };

            var oldObject = _panelStore.Find(x => x.Id == request.Id);
            oldObject.Should().NotBeNull();
            var oldName = oldObject?.Name;
            var oldDescription = oldObject?.Description;
            var oldPrice = oldObject?.Price;
            var oldPriceVisibility = oldObject?.PriceVisibility;
            var oldVisibility = oldObject?.Visibility;
            var oldTestSelectionId = oldObject?.TestSelectionId;
            var oldCurrencyId = oldObject?.CurrencyId;
            var oldTests = oldObject?.Tests;

            var result = await updateHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _panelStore.Find(x => x.Id == result.Id);
            verifiedObject.Should().NotBeNull();
            verifiedObject?.Id.Should().Be(request.Id);
            verifiedObject?.Name.Should().Be(request.Name);
            verifiedObject?.Tests.Select(x => x.Id).SequenceEqual(request.TestIds).Should().BeTrue();
            verifiedObject?.Price.Should().Be(request.Price);
            verifiedObject?.PriceVisibility.Should().Be(request.PriceVisibility);
            verifiedObject?.Visibility.Should().Be(request.Visibility);
            verifiedObject?.TestSelection.Id.Should().Be(request.TestSelectionId);
            verifiedObject?.Currency?.Id.Should().Be(request.CurrencyId);

            verifiedObject?.Name.Should().NotBe(oldName);
            verifiedObject?.Tests.Select(x => x.Id).SequenceEqual(oldTests!.Select(o => o.Id)).Should().BeFalse();
            verifiedObject?.Price.Should().NotBe(oldPrice);
            verifiedObject?.PriceVisibility.Should().NotBe(oldPriceVisibility);
            verifiedObject?.Visibility.Should().NotBe(oldVisibility);
            verifiedObject?.TestSelection.Id.Should().NotBe(oldTestSelectionId);
            verifiedObject?.Currency?.Id.Should().NotBe(oldCurrencyId);

            scPanelRepositoryMock.Verify(m => m.Update(It.IsAny<SC_Panel>()), Times.Once);
            scPanelRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
            scTestRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
            scTestSelectionRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
            scCurrencyRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
        }
    }
}
