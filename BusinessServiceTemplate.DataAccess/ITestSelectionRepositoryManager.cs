using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;

namespace BusinessServiceTemplate.DataAccess
{
    public interface ITestSelectionRepositoryManager : IDisposable
    {
        IScPanelRepository ScPanelRepository { get; }
        IScTestRepository ScTestRepository { get; }
        IScPanelTestRepository ScPanelTestRepository { get; }
        IScTestSelectionRepository ScTestSelectionRepository { get; }
        IScCurrencyRepository ScCurrencyRepository { get; }
        Task Save();
    }
}
