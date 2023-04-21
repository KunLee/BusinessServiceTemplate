using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess.Interfaces;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces
{
    public interface IScPanelTestRepository : IRepositoryBase<SC_Panel_Test>
    {
        Task<SC_Panel_Test?> FindByIds(int panelId, int testId);
    }
}
