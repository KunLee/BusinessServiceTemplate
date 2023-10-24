using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.Core.Services.Interfaces;
using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.Shared.Common;
using MediatR;

namespace BusinessServiceTemplate.Core.Handlers
{
    public  class ExportExcelDataHandler : IRequestHandler<ExportExcelDataRequest, MemoryStream>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IImportExportService _importExportService;
        public ExportExcelDataHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, IImportExportService importExportService)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _importExportService = importExportService;
        }
        public async Task<MemoryStream> Handle(ExportExcelDataRequest request, CancellationToken cancellationToken)
        {
            switch (request.type) 
            {
                case ImportExportDataType.MBS:
                    {
                        var mbs = (await _testSelectionRepositoryManager.ScMbsRepository.FindAll(false)).ToList();
                        if (mbs?.Count > 0) return _importExportService.CreateExcelFile(mbs);
                        break;
                    }
                case ImportExportDataType.AMA:
                    {
                        var ama = (await _testSelectionRepositoryManager.ScAmaRepository.FindAll(false)).ToList();
                        if (ama?.Count > 0) return _importExportService.CreateExcelFile(ama);
                        break;
                    }
            }

            return null;
        }
    }
}
