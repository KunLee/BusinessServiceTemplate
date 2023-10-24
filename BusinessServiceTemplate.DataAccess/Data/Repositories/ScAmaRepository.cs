using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScAmaRepository : RepositoryBase<SC_AMA>, IScAmaRepository
    {
        public ScAmaRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
