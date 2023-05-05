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
    public class UpdateTestHandlerTests
    {
        private List<SC_Panel> _panelStore;
        private List<SC_Test> _testStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public UpdateTestHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PanelDataToDomainMapper>();
                cfg.AddProfile<TestDataToDomainMapper>();
            });

            _panelStore = StoreFactory.PanelStore;
            _testStore = StoreFactory.TestStore;
        }

        [Fact]
        public async Task WhenClientTriggeringTestCreate_ThenNewTestAdded_ReturnTheCreatedTest() 
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();
            var scTestRepositoryMock = new Mock<IScTestRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p))).Verifiable();

            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p))).Verifiable();
            
            scTestRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Test>()))
                .Returns((SC_Test p) => 
                {
                    var testToUpdate = _testStore.Find(x => x.Id == p.Id);
                    Assert.NotNull(testToUpdate);
                    testToUpdate.Name = p.Name;
                    testToUpdate.Description = p.Description;
                    testToUpdate.DescriptionVisibility= p.DescriptionVisibility;
                    testToUpdate.Panels = p.Panels;
                    return Task.FromResult(testToUpdate);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var updateHandler = new UpdateTestHandler(unitOfWorkMock.Object, autoMapper);

            var request = new UpdateTestRequest
            {
                Id= 1,
                Name = "Update Test",
                Description = "Update Test Desc",
                DescriptionVisibility = false,
                PanelIds = new List<int> { 3 }
            };

            var oldObject = _testStore.Find(x => x.Id == request.Id);
            var oldName = oldObject?.Name;
            var oldDescription = oldObject?.Description;
            var oldDescriptionVisibility = oldObject?.DescriptionVisibility;
            var oldPanels = oldObject?.Panels;

            var result = await updateHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _testStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().NotBeNull();
            verifiedObject?.Name.Should().Be(request.Name);
            verifiedObject?.Description.Should().Be(request.Description);
            verifiedObject?.DescriptionVisibility.Should().Be(request.DescriptionVisibility);
            verifiedObject?.Panels.Select(x => x.Id).SequenceEqual(request.PanelIds).Should().BeTrue();

            verifiedObject?.Name.Should().NotBe(oldName);
            verifiedObject?.Description.Should().NotBe(oldDescription);
            verifiedObject?.DescriptionVisibility.Should().NotBe(oldDescriptionVisibility);
            verifiedObject?.Panels.Select(x => x.Id).SequenceEqual(oldPanels!.Select(o => o.Id)).Should().BeFalse();

            // Verify
            scTestRepositoryMock.Verify(m => m.Update(It.IsAny<SC_Test>()), Times.Once);
            scTestRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
            scPanelRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
        }
    }
}
