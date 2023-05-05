using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScPanelTestRepository : RepositoryBase<SC_Panel_Test>, IScPanelTestRepository
    {
        private new readonly TestSelectionRepositoryContext _repositoryContext;

        public ScPanelTestRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<SC_Panel_Test?> FindByIds(int panelId, int testId) => 
            await _repositoryContext.SC_Panel_Tests.FirstOrDefaultAsync(i => i.PanelId == panelId && i.TestId == testId);
    }
}
