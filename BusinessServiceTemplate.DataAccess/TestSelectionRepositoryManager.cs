using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess
{
    public class TestSelectionRepositoryManager : ITestSelectionRepositoryManager
    {
        private readonly TestSelectionRepositoryContext _testSelectionRepositoryContext;
        private IScPanelRepository? _scPanelRepository;
        private IScTestRepository? _scTestRepository;
        private IScPanelTestRepository? _scPanelTestRepository;
        private IScCurrencyRepository? _scCurrencyRepository;
        private IScTestSelectionRepository? _scTestSelectionRepository;
        private IScMbsRepository? _scMbsRepository;
        private IScAmaRepository? _scAmaRepository;
        private bool disposed = false;

        public TestSelectionRepositoryManager(TestSelectionRepositoryContext testSelectionRepositoryContext)
        {
            _testSelectionRepositoryContext = testSelectionRepositoryContext;
        }

        public DbContext DbContext
        {
            get
            {
                return _testSelectionRepositoryContext;
            }
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

        public IScCurrencyRepository ScCurrencyRepository
        {
            get
            {
                _scCurrencyRepository ??= new ScCurrencyRepository(_testSelectionRepositoryContext);
                return _scCurrencyRepository;
            }
        }

        public IScMbsRepository ScMbsRepository
        {
            get
            {
                _scMbsRepository ??= new ScMbsRepository(_testSelectionRepositoryContext);
                return _scMbsRepository;
            }
        }

        public IScAmaRepository ScAmaRepository
        {
            get
            {
                _scAmaRepository ??= new ScAmaRepository(_testSelectionRepositoryContext);
                return _scAmaRepository;
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
