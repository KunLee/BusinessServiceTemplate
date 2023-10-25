using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.Core.Services.Interfaces;
using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class ImportExcelHandler : IRequestHandler<ImportExcelRequest, List<ImportErrorResponseDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IImportExportService _importExportService;
        private readonly IMapper _mapper;

        public ImportExcelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, IImportExportService importExportService,
            IMapper mapper)
        {
            this._testSelectionRepositoryManager = testSelectionRepositoryManager;
            this._importExportService = importExportService;
            this._mapper = mapper;
        }
        public async Task<List<ImportErrorResponseDto>> Handle(ImportExcelRequest request, CancellationToken cancellationToken)
        {
            switch (request.Type)
            {
                case ImportExportDataType.MBS:
                    {
                        return await MbsValidateAndProcessing(request.ExcelFile);
                    }
                case ImportExportDataType.AMA:
                    {
                        return await AmaValidateAndProcessing(request.ExcelFile);
                    }
            }

            return null;
        }
        private async Task<List<ImportErrorResponseDto>> AmaValidateAndProcessing(IFormFile file) 
        {
            var validationErrors = new List<ImportErrorResponseDto>();

            var amaList = await _importExportService.ImportExcelFile(file, (excelRow, headers) =>
            {
                var ama = new AmaDto
                {
                    AMACode = ValidateStringFromCell(excelRow, headers, "AMACode", validationErrors),
                    Description = ValidateStringFromCell(excelRow, headers, "Description", validationErrors),
                    AMAFee = ValidateDecimalFromCell(excelRow, headers, "AMA Fee", validationErrors),
                    MedicareItem = ValidateIntegerFromCell(excelRow, headers, "Medicare Item", validationErrors),
                    ScheduleFee = ValidateDecimalFromCell(excelRow, headers, "Schedule Fee", validationErrors)
                };
                return ama;
            });

            if (validationErrors.Count > 0) return validationErrors;

            var mappedAmaList = amaList.Select(_mapper.Map<SC_AMA>);

            var amaQuery = await _testSelectionRepositoryManager.ScAmaRepository.FindAll();

            var amaEntities = amaQuery.ToList();

            var amaToUpdate = amaEntities.IntersectBy(mappedAmaList.Select(x => x.AMACode), mbs => mbs.AMACode);

            var amaToCreate = mappedAmaList.ExceptBy(amaEntities.Select(x => x.AMACode), mbs => mbs.AMACode);

            var amaToDelete = amaEntities.ExceptBy(mappedAmaList.Select(x => x.AMACode), mbs => mbs.AMACode);

            foreach (var amaEntity in amaToUpdate)
            {
                _testSelectionRepositoryManager.DbContext?.Entry(amaEntity).CurrentValues.SetValues(mappedAmaList.First(x => x.AMACode == amaEntity.AMACode));
            }

            foreach (var amaEntity in amaToCreate)
            {
                await _testSelectionRepositoryManager.ScAmaRepository.Create(amaEntity);
            }

            foreach (var amaEntity in amaToDelete)
            {
                await _testSelectionRepositoryManager.ScAmaRepository.Delete(amaEntity);
            }

            await _testSelectionRepositoryManager.Save();

            return validationErrors;
        }

        private async Task<List<ImportErrorResponseDto>> MbsValidateAndProcessing(IFormFile file)
        {
            var validationErrors = new List<ImportErrorResponseDto>();

            var mbsList = await _importExportService.ImportExcelFile(file, (excelRow, headers) =>
            {
                var mbs = new MbsDto
                {
                    ItemNum = ValidateIntegerFromCell(excelRow, headers, "ItemNum", validationErrors),
                    Category = ValidateIntegerFromCell(excelRow, headers, "Category", validationErrors),
                    Group = ValidateStringFromCell(excelRow, headers, "Group", validationErrors),
                    ItemType = ValidateStringFromCell(excelRow, headers, "ItemType", validationErrors),
                    FeeType = ValidateStringFromCell(excelRow, headers, "FeeType", validationErrors),
                    ScheduleFee = ValidateDoubleFromCell(excelRow, headers, "ScheduleFee", validationErrors),
                    Description = ValidateStringFromCell(excelRow, headers, "Description", validationErrors),
                };
                return mbs;
            });

            if (validationErrors.Count > 0) return validationErrors;

            var mappedMbsList = mbsList.Select(_mapper.Map<SC_MBS>);

            var mbsQuery = await _testSelectionRepositoryManager.ScMbsRepository.FindAll();

            var mbsEntities = mbsQuery.ToList();

            var mbsToUpdate = mbsEntities.IntersectBy(mappedMbsList.Select(x => x.ItemNum), mbs => mbs.ItemNum);

            var mbsToCreate = mappedMbsList.ExceptBy(mbsEntities.Select(x => x.ItemNum), mbs => mbs.ItemNum);

            var mbsToDelete = mbsEntities.ExceptBy(mappedMbsList.Select(x => x.ItemNum), mbs => mbs.ItemNum);

            foreach (var mbsEntity in mbsToUpdate) 
            {
                _testSelectionRepositoryManager.DbContext?.Entry(mbsEntity).CurrentValues.SetValues(mappedMbsList.First(x => x.ItemNum == mbsEntity.ItemNum));
            }

            foreach (var mbsEntity in mbsToCreate)
            {
                await _testSelectionRepositoryManager.ScMbsRepository.Create(mbsEntity);
            }

            foreach (var mbsEntity in mbsToDelete)
            {
                await _testSelectionRepositoryManager.ScMbsRepository.Delete(mbsEntity);
            }

            await _testSelectionRepositoryManager.Save();

            return validationErrors;
        }

        //private (List<T> Create, List<T> Update, List<T> Delete) ValidateEntitiesWithImport<T>(List<T> import, List<T> entities) 
        //{
        //    import.Intersect(entities);
        //}
        private string ValidateStringFromCell(IRow row, List<string> properties, string fieldName, List<ImportErrorResponseDto> validationErrors)
        {
            DataFormatter formatter = new();
            return formatter.FormatCellValue(row.GetCell(properties.FindIndex(x => x.Equals(fieldName))));
        }

        private int ValidateIntegerFromCell(IRow row, List<string> properties, string fieldName, List<ImportErrorResponseDto> validationErrors)
        {
            int value = -1;

            DataFormatter formatter = new();
            String cellValue = formatter.FormatCellValue(row.GetCell(properties.FindIndex(x => x.Equals(fieldName))));

            if (!string.IsNullOrEmpty(cellValue) && Int32.TryParse(cellValue, out value))
            {
                return value;
            }
            else
            {
                validationErrors.Add(new ImportErrorResponseDto()
                {
                    RowNumber = row.RowNum,
                    Field = fieldName,
                    Error = "Value invalid"
                });
            }
            return value;
        }

        private decimal ValidateDecimalFromCell(IRow row, List<string> properties, string fieldName, List<ImportErrorResponseDto> validationErrors)
        {
            decimal value = -1;

            DataFormatter formatter = new();
            var cellValue = formatter.FormatCellValue(row.GetCell(properties.FindIndex(x => x.Equals(fieldName))));

            if (!string.IsNullOrEmpty(cellValue) && decimal.TryParse(cellValue, out value))
            {
                return value;
            }
            else
            {
                validationErrors.Add(new ImportErrorResponseDto()
                {
                    RowNumber = row.RowNum,
                    Field = fieldName,
                    Error = "Value invalid"
                });
            }
            return value;
        }

        private double ValidateDoubleFromCell(IRow row, List<string> properties, string fieldName, List<ImportErrorResponseDto> validationErrors)
        {
            double value = -1;

            DataFormatter formatter = new();
            var cellValue = formatter.FormatCellValue(row.GetCell(properties.FindIndex(x => x.Equals(fieldName))));

            if (!string.IsNullOrEmpty(cellValue) && double.TryParse(cellValue, out value))
            {
                return value;
            }
            else
            {
                validationErrors.Add(new ImportErrorResponseDto()
                {
                    RowNumber = row.RowNum,
                    Field = fieldName,
                    Error = "Value invalid"
                });
            }
            return value;
        }
    }
}
