using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Data.Repositories;

namespace BusinessServiceTemplate.DataAccess
{
    public class TestSelectionRepositoryManager : ITestSelectionRepositoryManager
    {
        private TestSelectionRepositoryContext _testSelectionRepositoryContext;
        private IScPanelRepository _scPanelRepository;
        private IScTestRepository _scTestRepository;
        private IScTestSelectionRepository _scTestSelectionRepository;

        public TestSelectionRepositoryManager(TestSelectionRepositoryContext testSelectionRepositoryContext)
        {
            _testSelectionRepositoryContext = testSelectionRepositoryContext;
        }

        public IScPanelRepository ScPanelRepository
        {
            get
            {
                _scPanelRepository ??= new ScPanelRepository(_testSelectionRepositoryContext);
                return _scPanelRepository;
            }
        }

        public IScTestRepository ScTestRepository
        {
            get
            {
                _scTestRepository ??= new ScTestRepository(_testSelectionRepositoryContext);
                return _scTestRepository;
            }
        }

        public IScTestSelectionRepository ScTestSelectionRepository
        {
            get
            {
                _scTestSelectionRepository ??= new ScTestSelectionRepository(_testSelectionRepositoryContext);
                return _scTestSelectionRepository;
            }
        }

        public async Task Save() => await _testSelectionRepositoryContext.SaveChangesAsync();
    }
}
