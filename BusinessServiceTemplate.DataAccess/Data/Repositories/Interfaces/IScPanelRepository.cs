using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess.Interfaces;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces
{
    public interface IScPanelRepository : IRepositoryBase<SC_Panel>
    {
        Task<SC_Panel?> FindById(int id);
    }
}
