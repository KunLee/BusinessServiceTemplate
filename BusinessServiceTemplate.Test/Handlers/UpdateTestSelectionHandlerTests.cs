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
    public class UpdateTestSelectionHandlerTests
    {
        private List<SC_TestSelection> _testSelectionStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public UpdateTestSelectionHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestSelectionDataToDomainMapper>();
            });

            _testSelectionStore = StoreFactory.TestSelectionStore;
        }

        [Fact]
        public async Task WhenClientTriggeringTestSelectionUpdate_ThenSpecificTestSelectionUpdated_ReturnTheUpdatedTestSelection() 
        {
            // Mock
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();

            // Setup
            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p))).Verifiable();

            scTestSelectionRepositoryMock.Setup(m => m.Update(It.IsAny<SC_TestSelection>()))
                .Returns((SC_TestSelection p) => 
                {
                    var testSelectionToUpdate = _testSelectionStore.Find(x => x.Id == p.Id);
                    Assert.NotNull(testSelectionToUpdate);
                    testSelectionToUpdate.Name = p.Name;
                    testSelectionToUpdate.Description = p.Description;
                    testSelectionToUpdate.SpecialityId = p.SpecialityId;
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var updateHandler = new UpdateTestSelectionHandler(unitOfWorkMock.Object, autoMapper);

            var request = new UpdateTestSelectionRequest
            {
                Id = 1,
                Name = "Update Test Selection",
                Description = "Update Test Selection Desc",
                DescriptionVisibility = false,
                SpecialityId = 2
            };

            var oldObject = _testSelectionStore.Find(x => x.Id == request.Id);
            var oldName = oldObject?.Name;
            var oldDescriptionVisibility = oldObject?.DescriptionVisibility;
            var oldSpecialityId = oldObject?.SpecialityId;

            var result = await updateHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _testSelectionStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().NotBeNull();
            verifiedObject?.Name.Should().Be(request.Name);
            verifiedObject?.DescriptionVisibility.Should().Be(request.DescriptionVisibility);
            verifiedObject?.SpecialityId.Should().Be(request.SpecialityId);

            verifiedObject?.Name.Should().NotBe(oldName);
            verifiedObject?.DescriptionVisibility.Should().NotBe(oldDescriptionVisibility);
            verifiedObject?.SpecialityId.Should().NotBe(oldSpecialityId);

            // Verify
            scTestSelectionRepositoryMock.Verify(m => m.Update(It.IsAny<SC_TestSelection>()), Times.Once);
            scTestSelectionRepositoryMock.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
        }
    }
}
