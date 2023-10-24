using BusinessServiceTemplate.DataAccess.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess
{
    public interface ITestSelectionRepositoryManager : IDisposable
    {
        DbContext DbContext { get; }
        IScPanelRepository ScPanelRepository { get; }
        IScTestRepository ScTestRepository { get; }
        IScPanelTestRepository ScPanelTestRepository { get; }
        IScTestSelectionRepository ScTestSelectionRepository { get; }
        IScCurrencyRepository ScCurrencyRepository { get; }
        IScMbsRepository ScMbsRepository { get; }
        IScAmaRepository ScAmaRepository { get; }
        Task Save();
    }
}
