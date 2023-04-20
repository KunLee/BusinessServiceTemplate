using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;

namespace BusinessServiceTemplate.DataAccess
{
    public interface ITestSelectionRepositoryManager : IDisposable
    {
        IScPanelRepository ScPanelRepository { get; }
        IScTestRepository ScTestRepository { get; }
        IScTestSelectionRepository ScTestSelectionRepository { get; }
        Task Save();
    }
}
