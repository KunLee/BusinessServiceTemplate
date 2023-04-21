using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.DataAccess.Interfaces;

namespace BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces
{
    public interface IScTestRepository : IRepositoryBase<SC_Test>
    {
        Task<SC_Test?> FindById(int id);
    }
}
