using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories
{
    public class ScPanelTestRepository : RepositoryBase<SC_Panel_Test>, IScPanelTestRepository
    {
        public ScPanelTestRepository(TestSelectionRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
