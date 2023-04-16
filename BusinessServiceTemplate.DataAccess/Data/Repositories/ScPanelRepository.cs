using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScPanelRepository : RepositoryBase<SC_Panel>, IScPanelRepository
    {
        public ScPanelRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
