using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces
{
    public interface IScMbsRepository : IRepositoryBase<SC_MBS>
    {
        Task<SC_MBS?> FindByItemNum(int itemNum);
        Task<SC_MBS> FindMbsAmaByItemNum(int itemNum);
    }
}
