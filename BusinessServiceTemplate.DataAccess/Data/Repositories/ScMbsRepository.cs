using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScMbsRepository : RepositoryBase<SC_MBS>, IScMbsRepository
    {
        private new readonly TestSelectionRepositoryContext _repositoryContext;

        public ScMbsRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<SC_MBS?> FindByItemNum(int itemNum)
        {
            return await _repositoryContext.SC_MBS.FirstOrDefaultAsync(i => i.ItemNum == itemNum);
        }

        public async Task<SC_MBS> FindMbsAmaByItemNum(int itemNum)
        {
            return await _repositoryContext.SC_MBS.Include(i => i.AustralianMedicalAssociations).FirstOrDefaultAsync(i => i.ItemNum == itemNum);
        }
    }
}
