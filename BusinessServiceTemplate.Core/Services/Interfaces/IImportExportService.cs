using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;

namespace BusinessServiceTemplate.Core.Services.Interfaces
{
    public interface IImportExportService
    {
        Task<List<T>> ImportExcelFile<T>(IFormFile excelFile, Func<IRow, List<string>, T> mapFunction);
        MemoryStream CreateExcelFile<T>(List<T> data);
    }
}
