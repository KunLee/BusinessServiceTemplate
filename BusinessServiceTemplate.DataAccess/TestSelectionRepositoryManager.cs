using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Data.Repositories;

namespace BusinessServiceTemplate.DataAccess
{
    public class TestSelectionRepositoryManager : ITestSelectionRepositoryManager
    {
        private readonly TestSelectionRepositoryContext _testSelectionRepositoryContext;
        private IScPanelRepository? _scPanelRepository;
        private IScTestRepository? _scTestRepository;
        private IScPanelTestRepository? _scPanelTestRepository;
        private IScTestSelectionRepository? _scTestSelectionRepository;
        private bool disposed = false;

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

        public IScPanelTestRepository ScPanelTestRepository
        {
            get
            {
                _scPanelTestRepository ??= new ScPanelTestRepository(_testSelectionRepositoryContext);
                return _scPanelTestRepository;
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

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _testSelectionRepositoryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
