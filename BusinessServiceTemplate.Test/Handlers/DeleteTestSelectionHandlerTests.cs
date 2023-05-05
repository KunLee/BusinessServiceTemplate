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
    public class DeleteTestSelectionHandlerTests
    {
        private List<SC_TestSelection> _testSelectionStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public DeleteTestSelectionHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestSelectionDataToDomainMapper>();
            });

            _testSelectionStore = StoreFactory.TestSelectionStore;
        }

        [Fact]
        public async Task WhenClientTriggeringTestSelectionDelete_ThenSpecificTestSelectionDeleted_ReturnTheDeletedTestSelection() 
        {
            // Mock
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();

            // Setup
            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p)));

            scTestSelectionRepositoryMock.Setup(m => m.Delete(It.IsAny<SC_TestSelection>()))
                .Returns((SC_TestSelection p) => 
                {
                    _testSelectionStore.Remove(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var deleteHandler = new DeleteTestSelectionHandler(unitOfWorkMock.Object, autoMapper);

            var request = new DeleteTestSelectionRequest
            {
                Id = 5
            };

            // Assert
            var existedObject = _testSelectionStore.Find(x => x.Id == request.Id);
            existedObject.Should().NotBeNull();

            // Sut
            var result = await deleteHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _testSelectionStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().BeNull();

            scTestSelectionRepositoryMock.Verify(m => m.Delete(It.IsAny<SC_TestSelection>()), Times.Once);
        }
    }
}
