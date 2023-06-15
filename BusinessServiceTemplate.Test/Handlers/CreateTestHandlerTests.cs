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
    public class CreateTestHandlerTests
    {
        private List<SC_Panel> _panelStore;
        private List<SC_Test> _testStore;
        private List<SC_TestSelection> _testSelectionStore;
        private List<SC_Currency> _currencyStore;
        private IdGenerator _idGenerator;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public CreateTestHandlerTests()
        {
            _idGenerator = new IdGenerator(10);

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
        public async Task WhenClientTriggeringTestCreate_ThenNewTestAdded_ReturnTheCreatedTest() 
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();
            var scTestRepositoryMock = new Mock<IScTestRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p)));

            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p)));

            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p)));
            
            scTestRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Test>()))
                .Returns((SC_Test p) => 
                {
                    p.Id = _idGenerator.Next();
                    _testStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            scTestRepositoryMock.Setup(m => m.Any(It.IsAny<Expression<Func<SC_Test, bool>>>()))
                .Returns((Expression<Func<SC_Test, bool>> p) => Task.FromResult(_testStore.Any(p.Compile()))).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScCurrencyRepository).Returns(scCurrencyRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateTestHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateTestRequest
            {
                Name = "New Test",
                Description = "New Test Desc",
                DescriptionVisibility = false,
                PanelIds = new List<int> { 1, 2, 3 }
            };

            var result = await createHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _testStore.Find(x=> x.Id == result.Id);
            Assert.NotNull(verifiedObject);
            verifiedObject.Id.Should().Be(_idGenerator.Last());
            verifiedObject.Name.Should().Be(request.Name);
            verifiedObject.DescriptionVisibility.Should().Be(request.DescriptionVisibility);
            verifiedObject.Panels.Select(x => x.Id).SequenceEqual(request.PanelIds).Should().BeTrue();

            scTestRepositoryMock.Verify(m => m.Any(It.IsAny<Expression<Func<SC_Test, bool>>>()), Times.Once);
            scTestRepositoryMock.Verify(m => m.Create(It.IsAny<SC_Test>()), Times.Once);
        }

        [Fact]
        public async Task WhenClientTriggeringTestCreate_ThenDuplicateTestFound_ThrowException()
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();
            var scTestRepositoryMock = new Mock<IScTestRepository>();
            var scCurrencyRepositoryMock = new Mock<IScCurrencyRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p)));

            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p))).Verifiable();

            scCurrencyRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
               .Returns((int p) => Task.FromResult(_currencyStore.Find(x => x.Id == p))).Verifiable();

            scTestRepositoryMock.Setup(m => m.Any(It.IsAny<Expression<Func<SC_Test, bool>>>()))
                .Returns((Expression<Func<SC_Test, bool>> p) => Task.FromResult(_testStore.Any(p.Compile()))).Verifiable();

            scTestRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Test>()))
                .Returns((SC_Test p) =>
                {
                    p.Id = _idGenerator.Next();
                    _testStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateTestHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateTestRequest
            {
                Description = "Test Duplicate Desc",
                DescriptionVisibility = true,
                Name = "Test Duplicate",
                PanelIds = new List<int> { 1, 2 }
            };

            // Sut
            Func<Task> act = () => createHandler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                        .Where(e => e.Message.StartsWith(ConstantStrings.DUPLICATE_REQUEST_DATA));

            scTestRepositoryMock.Verify(m => m.Any(It.IsAny<Expression<Func<SC_Test, bool>>>()), Times.Once);
            scTestRepositoryMock.Verify(m => m.Create(It.IsAny<SC_Test>()), Times.Never);
        }
    }
}
