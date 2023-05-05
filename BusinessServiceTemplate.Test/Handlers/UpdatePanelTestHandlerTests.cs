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
    public class UpdatePanelTestHandlerTests
    {
        private List<SC_Panel_Test> _panelTestStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public UpdatePanelTestHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PanelTestDataToDomainMapper>();
            });

            _panelTestStore = StoreFactory.PanelTestStore;
        }

        [Fact]
        public async Task WhenClientTriggeringPanelTestUpdate_ThenSpecificPanelTestUpdated_ReturnTheUpdatedPanelTest() 
        {
            // Mock
            var scPanelTestRepositoryMock = new Mock<IScPanelTestRepository>();

            // Setup
            scPanelTestRepositoryMock.Setup(m => m.FindByIds(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((int p, int t) => Task.FromResult(_panelTestStore.Find(x => x.PanelId == p && x.TestId == t)))
            .Verifiable();

            scPanelTestRepositoryMock.Setup(m => m.UpdateChanges(It.IsAny<SC_Panel_Test>()))
            .Callback((SC_Panel_Test p) => 
            {
                var panelTestToUpdate = _panelTestStore.Find(x => x.PanelId == p.PanelId && x.TestId == p.TestId);
                Assert.NotNull(panelTestToUpdate);
                panelTestToUpdate.Visibility = p.Visibility;
            }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelTestRepository).Returns(scPanelTestRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var updateHandler = new UpdatePanelTestHandler(unitOfWorkMock.Object, autoMapper);

            var request = new UpdatePanelTestRequest
            {
                PanelId = 1,
                TestId = 1,
                Visibility = false
            };

            var oldObject = _panelTestStore.Find(x => x.PanelId == request.PanelId && x.TestId == request.TestId);
            var oldVisibility = oldObject?.Visibility;

            var result = await updateHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _panelTestStore.Find(x => x.PanelId == request.PanelId && x.TestId == request.TestId);

            verifiedObject?.Visibility.Should().Be(request.Visibility);
            verifiedObject?.Visibility.Should().NotBe(oldVisibility);

            // Verify
            scPanelTestRepositoryMock.Verify(m => m.UpdateChanges(It.IsAny<SC_Panel_Test>()), Times.Once);
            scPanelTestRepositoryMock.Verify(m => m.FindByIds(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
