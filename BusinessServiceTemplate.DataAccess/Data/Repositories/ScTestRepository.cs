using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScTestRepository : RepositoryBase<SC_Test>, IScTestRepository
    {
        private readonly TestSelectionRepositoryContext _repositoryContext;
        public ScTestRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<SC_Test?> FindById(int id) => await _repositoryContext.SC_Tests.Include(x => x.Panels).FirstOrDefaultAsync(i => i.Id == id);
    }
}
