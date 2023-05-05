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
using System.Linq.Expressions;

namespace BusinessServiceTemplate.Test.Handlers
{
    public class CreateTestSelectionHandlerTests
    {
        private List<SC_TestSelection> _testSelectionStore;
        private IdGenerator _idGenerator;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public CreateTestSelectionHandlerTests()
        {
            _idGenerator = new IdGenerator(10);

            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestSelectionDataToDomainMapper>();
            });

            _testSelectionStore = StoreFactory.TestSelectionStore;
        }

        [Fact]
        public async Task WhenClientTriggeringTestSelectionCreate_ThenNewTestSelectionAdded_ReturnTheCreatedTestSelection() 
        {
            // Mock
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();

            // Setup
            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p)));

            scTestSelectionRepositoryMock.Setup(m => m.Create(It.IsAny<SC_TestSelection>()))
                .Returns((SC_TestSelection p) => 
                {
                    p.Id = _idGenerator.Next();
                    _testSelectionStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateTestSelectionHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateTestSelectionRequest
            {
                Name = "New Test Selection 1",
                Description = "New Test Selection 1 Desc",
                DescriptionVisibility = false,
                SpecialityId = 1
            };

            var result = await createHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _testSelectionStore.Find(x=> x.Id == result.Id);
            verifiedObject.Should().NotBeNull();
            verifiedObject?.Id.Should().Be(_idGenerator.Last());
            verifiedObject?.Name.Should().Be(request.Name);
            verifiedObject?.DescriptionVisibility.Should().Be(request.DescriptionVisibility);
            verifiedObject?.SpecialityId.Should().Be(request.SpecialityId);

            scTestSelectionRepositoryMock.Verify(m => m.Create(It.IsAny<SC_TestSelection>()), Times.Once);
        }

        [Fact]
        public async Task WhenClientTriggeringTestSelectionCreate_ThenDuplicateTestSelectionFound_ExceptionThrown()
        {
            // Mock
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();

            // Setup
            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p)));

            scTestSelectionRepositoryMock.Setup(m => m.Any(It.IsAny<Expression<Func<SC_TestSelection, bool>>>()))
                .Returns((Expression<Func<SC_TestSelection, bool>> p) => Task.FromResult(_testSelectionStore.Any(p.Compile()))).Verifiable();

            scTestSelectionRepositoryMock.Setup(m => m.Create(It.IsAny<SC_TestSelection>()))
                .Returns((SC_TestSelection p) =>
                {
                    p.Id = _idGenerator.Next();
                    _testSelectionStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateTestSelectionHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateTestSelectionRequest
            {
                Name = "Test Selection Duplicate",
                Description = "Test Selection Duplicate Desc",
                DescriptionVisibility = true,
                SpecialityId = 4
            };

            // Sut
            Func<Task> act = () => createHandler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                        .Where(e => e.Message.StartsWith(ConstantStrings.DUPLICATE_REQUEST_DATA));

            scTestSelectionRepositoryMock.Verify(m => m.Any(It.IsAny<Expression<Func<SC_TestSelection, bool>>>()), Times.Once);
            scTestSelectionRepositoryMock.Verify(m => m.Create(It.IsAny<SC_TestSelection>()), Times.Never);
        }
    }
}
