using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScCurrencyRepository : RepositoryBase<SC_Currency>, IScCurrencyRepository
    {
        public ScCurrencyRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
