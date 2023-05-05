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
    public class CreatePanelHandlerTests
    {
        private List<SC_Panel> _panelStore;
        private List<SC_Test> _testStore;
        private List<SC_TestSelection> _testSelectionStore;
        private IdGenerator _idGenerator;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public CreatePanelHandlerTests()
        {
            _idGenerator = new IdGenerator(10);

            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PanelDataToDomainMapper>();
                cfg.AddProfile<TestDataToDomainMapper>();
            });

            _panelStore = StoreFactory.PanelStore;
            _testStore = StoreFactory.TestStore;
            _testSelectionStore = StoreFactory.TestSelectionStore;
        }

        [Fact]
        public async Task WhenClientTriggeringPanelCreate_ThenNewPanelAdded_ReturnTheCreatedPanel() 
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();
            var scTestRepositoryMock = new Mock<IScTestRepository>();
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p))).Verifiable();

            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p))).Verifiable();

            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p))).Verifiable();
            
            scPanelRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Panel>()))
                .Returns((SC_Panel p) => 
                {
                    p.Id = _idGenerator.Next();
                    _panelStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            scPanelRepositoryMock.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<SC_Panel, bool>>>(), true))
                .Returns((Expression<Func<SC_Panel, bool>> expression, bool notrace) =>
                {
                    return Task.FromResult(_panelStore.Where(expression.Compile()).AsQueryable());

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreatePanelHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreatePanelRequest
            {
                Name = "New Panel",
                Description = "New Panel Desc",
                DescriptionVisibility = false,
                Price = 100.01m,
                PriceVisibility = false,
                TestIds = new List<int> { 1, 2, 3 },
                TestSelectionId = 1,
                Visibility = true
            };

            var result = await createHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _panelStore.Find(x=> x.Id == result.Id);
            Assert.NotNull(verifiedObject);
            verifiedObject.Id.Should().Be(_idGenerator.Last());
            verifiedObject.Name.Should().Be(request.Name);
            verifiedObject.Tests.Select(x => x.Id).SequenceEqual(request.TestIds).Should().BeTrue();
            verifiedObject.Price.Should().Be(request.Price);
            verifiedObject.PriceVisibility.Should().Be(request.PriceVisibility);
            verifiedObject.Visibility.Should().Be(request.Visibility);
            verifiedObject.TestSelection.Id.Should().Be(request.TestSelectionId);

            scPanelRepositoryMock.Verify(m => m.Create(It.IsAny<SC_Panel>()), Times.Once);
            scPanelRepositoryMock.Verify(m => m.FindByCondition(It.IsAny<Expression<Func<SC_Panel, bool>>>(), true), Times.Once);
        }

        [Fact]
        public async Task WhenClientTriggeringPanelCreate_ThenDuplicatePanelFound_ThrowException()
        {
            // Mock
            var scPanelRepositoryMock = new Mock<IScPanelRepository>();
            var scTestRepositoryMock = new Mock<IScTestRepository>();
            var scTestSelectionRepositoryMock = new Mock<IScTestSelectionRepository>();

            // Setup
            scPanelRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_panelStore.Find(x => x.Id == p)));

            scTestRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testStore.Find(x => x.Id == p))).Verifiable();

            scTestSelectionRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_testSelectionStore.Find(x => x.Id == p))).Verifiable();

            scPanelRepositoryMock.Setup(m => m.Create(It.IsAny<SC_Panel>()))
                .Returns((SC_Panel p) =>
                {
                    p.Id = _idGenerator.Next();
                    _panelStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            scPanelRepositoryMock.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<SC_Panel, bool>>>(), true))
                .Returns((Expression<Func<SC_Panel, bool>> expression, bool notrace) =>
                {
                    return Task.FromResult(_panelStore.Where(expression.Compile()).AsQueryable());

                }).Verifiable();

            var unitOfWorkMock = new Mock<ITestSelectionRepositoryManager>();

            unitOfWorkMock.Setup(m => m.ScPanelRepository).Returns(scPanelRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestRepository).Returns(scTestRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.ScTestSelectionRepository).Returns(scTestSelectionRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreatePanelHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreatePanelRequest
            {
                Name = "Panel Duplicate",
                Description = "Panel Duplicate Desc",
                DescriptionVisibility = true,
                Price = 10.01m,
                PriceVisibility = true,
                TestSelectionId = 1,
                Visibility = true,
                TestIds = new List<int> { 1 }
            };

            // Sut
            Func<Task> act = () => createHandler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                        .Where(e => e.Message.StartsWith(ConstantStrings.DUPLICATE_REQUEST_DATA));
            
            scPanelRepositoryMock.Verify(m => m.FindByCondition(It.IsAny<Expression<Func<SC_Panel, bool>>>(), true), Times.Once);
            scPanelRepositoryMock.Verify(m => m.Create(It.IsAny<SC_Panel>()), Times.Never);
        }
    }
}
