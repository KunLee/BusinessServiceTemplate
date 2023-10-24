using BusinessServiceTemplate.Shared.Common;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class ExportExcelDataRequest : IRequest<MemoryStream>
    {
        public ImportExportDataType type { get; set; }

        public ExportExcelDataRequest(ImportExportDataType type)
        {
            this.type = type;
        }
    }
}
