using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScTestSelectionRepository : RepositoryBase<SC_TestSelection>, IScTestSelectionRepository
    {
        private readonly TestSelectionRepositoryContext _repositoryContext;
        public ScTestSelectionRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<SC_TestSelection?> FindById(int id) => 
            await _repositoryContext.SC_TestSelections.Include(x => x.Panels).FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IList<SC_TestSelection>> FindBySpecialityId(int specialityId)
        {
            var testSelectionList = await _repositoryContext.SC_TestSelections
                .Include(x => x.Panels)
                .Where(i => i.SpecialityId == specialityId)
                .ToListAsync();
            return testSelectionList;
        }
    }
}
