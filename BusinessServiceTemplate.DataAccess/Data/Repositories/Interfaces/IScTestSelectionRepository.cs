using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess.Interfaces;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces
{
    public interface IScTestSelectionRepository : IRepositoryBase<SC_TestSelection>
    {
        Task<SC_TestSelection?> FindById(int id);
        Task<IList<SC_TestSelection>> FindBySpecialityId(int specialityId);
    }
}
