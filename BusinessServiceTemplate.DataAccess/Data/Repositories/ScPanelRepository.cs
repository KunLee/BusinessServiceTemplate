using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScPanelRepository : RepositoryBase<SC_Panel>, IScPanelRepository
    {
        private readonly TestSelectionRepositoryContext _repositoryContext;
        public ScPanelRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<SC_Panel?> FindById(int id) => await _repositoryContext.SC_Panels.Include(x => x.Tests).FirstOrDefaultAsync(i => i.Id == id);
    }
}
