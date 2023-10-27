using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;

namespace BusinessServiceTemplate.Core.Services.Interfaces
{
    /// <summary>
    /// Provides a contract for an import and export service that allows data to be imported from and exported to Excel files.
    /// </summary>
    public interface IImportExportService
    {
        /// <summary>
        /// Imports data from an Excel file provided as an IFormFile and maps it to a list of entities of type T using a map function.
        /// </summary>
        /// <typeparam name="T">The type of entity to map the data to.</typeparam>
        /// <param name="excelFile">The Excel file to import data from as an IFormFile.</param>
        /// <param name="mapFunction">A mapping function that maps Excel rows to entities of type T, along with a list of validation errors.</param>
        /// <returns>A list of entities of type T.</returns>
        Task<List<T>> ImportExcelFile<T>(IFormFile excelFile, Func<IRow, List<string>, T> mapFunction);

        /// <summary>
        /// Creates an Excel file from a list of data of type T and returns it as a MemoryStream.
        /// </summary>
        /// <typeparam name="T">The type of entity contained in the list.</typeparam>
        /// <param name="data">The list of entities to export to the Excel file.</param>
        /// <returns>A MemoryStream containing the Excel file.</returns>
        MemoryStream CreateExcelFile<T>(List<T> data);
    }
}
