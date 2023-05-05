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
    public class DeletePanelHandlerTests
    {
        private List<SC_Panel> _panelStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public DeletePanelHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PanelDataToDomainMapper>();
                cfg.AddProfile<TestDataToDomainMapper>();
            });

            _panelStore = StoreFactory.PanelStore;
        }

        [Fact]
        public async Task WhenClientTriggeringPanelDelete_ThenSpecificPanelDeleted_ReturnTheDeletedPanel() 
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p)));

            scPanelRepositoryMock.Setup(m => m.Delete(It.IsAny<SC_Panel>()))
                .Returns((SC_Panel p) => 
                {
                    _panelStore.Remove(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var deleteHandler = new DeletePanelHandler(unitOfWorkMock.Object, autoMapper);

            var request = new DeletePanelRequest
            {
                Id = 5
            };

            // Assert
            var existedObject = _panelStore.Find(x => x.Id == request.Id);
            existedObject.Should().NotBeNull();

            // Sut
            var result = await deleteHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _panelStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().BeNull();

            scPanelRepositoryMock.Verify(m => m.Delete(It.IsAny<SC_Panel>()), Times.Once);
        }
    }
}
