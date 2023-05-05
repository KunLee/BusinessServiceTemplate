using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScPanelRepository : RepositoryBase<SC_Panel>, IScPanelRepository
    {
        private new readonly TestSelectionRepositoryContext _repositoryContext;
        public ScPanelRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        /// <summary>
        /// Fetch the SC_Panel record with the associated Test records
        /// * This should not be used/involved in Create/Update since it includes Tests
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SC_Panel?> FindByIdWithTests(int id) => await _repositoryContext.SC_Panels.Include(x => x.Tests).FirstOrDefaultAsync(i => i.Id == id);
    }
}
