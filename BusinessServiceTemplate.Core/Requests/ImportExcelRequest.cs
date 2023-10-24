using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BusinessServiceTemplate.Core.Requests
{
    public class ImportExcelRequest : IRequest<List<ImportErrorResponseDto>>
    {
        public IFormFile ExcelFile { get; set; }
        public ImportExportDataType Type { get; set; }

        public ImportExcelRequest(ImportExportDataType type, IFormFile excelFile)
        {
            this.Type = type;
            this.ExcelFile = excelFile;
        }
    }
}
