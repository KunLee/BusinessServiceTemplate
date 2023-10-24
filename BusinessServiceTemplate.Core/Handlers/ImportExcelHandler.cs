using AutoMapper;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.Core.Services.Interfaces;
using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.Util.Collections;
using NPOI.XWPF.UserModel;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                        var errorList = await MbsValidateAndProcessing(request.ExcelFile);
                        return errorList;
                    }
                case ImportExportDataType.AMA:
                    {
                        //await AmaValidateAndProcessing(request.ExcelFile);
                        break;
                    }
            }

            return null;
        }

        private async Task<List<ImportErrorResponseDto>> MbsValidateAndProcessing(IFormFile file)
        {
            //ItemNum, Category, Group, ItemType, FeeType, ScheduleFee, Description
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

            var mappedMbsList = mbsList.Select(_mapper.Map<SC_MBS>);

            var mbsQuery = await _testSelectionRepositoryManager.ScMbsRepository.FindAll();

            var mbsEntities = mbsQuery.ToList();

            var mbsToUpdate = mbsEntities.IntersectBy(mappedMbsList.Select(x => x.ItemNum), mbs => mbs.ItemNum);

            var mbsToCreate = mappedMbsList.ExceptBy(mbsEntities.Select(x => x.ItemNum), mbs => mbs.ItemNum);

            var mbsToDelete = mbsEntities.ExceptBy(mappedMbsList.Select(x => x.ItemNum), mbs => mbs.ItemNum);

            //_testSelectionRepositoryManager.DbContext?.Entry(newQuote).CurrentValues.SetValues(quoteToCopy);

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
