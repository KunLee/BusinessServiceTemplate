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
    public class DeleteTestHandlerTests
    {
        private List<SC_Test> _testStore;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public DeleteTestHandlerTests()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PanelDataToDomainMapper>();
                cfg.AddProfile<TestDataToDomainMapper>();
            });

            _testStore = StoreFactory.TestStore;
        }

        [Fact]
        public async Task WhenClientTriggeringTestDelete_ThenSpecificTestDeleted_ReturnTheDeletedTest() 
        {
            // Mock
            var scTestRepositoryMock = new Mock<IScTestRepository>();

            // Setup
            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p)));

            scTestRepositoryMock.Setup(m => m.Delete(It.IsAny<SC_Test>()))
                .Returns((SC_Test p) => 
                {
                    _testStore.Remove(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var deleteHandler = new DeleteTestHandler(unitOfWorkMock.Object, autoMapper);

            var request = new DeleteTestRequest
            {
                Id = 5
            };

            // Assert
            var existedObject = _testStore.Find(x => x.Id == request.Id);
            existedObject.Should().NotBeNull();

            // Sut
            var result = await deleteHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _testStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().BeNull();

            scTestRepositoryMock.Verify(m => m.Delete(It.IsAny<SC_Test>()), Times.Once);
        }
    }
}
