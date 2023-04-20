using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScTestSelectionRepository : RepositoryBase<SC_TestSelection>, IScTestSelectionRepository
    {
        public ScTestSelectionRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
