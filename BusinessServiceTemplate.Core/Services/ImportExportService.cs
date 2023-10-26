using BusinessServiceTemplate.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BusinessServiceTemplate.Core.Services
{
    public class ImportExportService : IImportExportService
    {
        public MemoryStream CreateExcelFile<T>(List<T> data)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Default");

            ICellStyle headerStyle = CreateHeaderStyle(workbook);

            var headers = sheet.CreateRow(0);

            var properties = typeof(T).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                CreateTableCell(headers, i, properties[i].Name, headerStyle);
            }

            ICellStyle style = CreateCellStyle(workbook);

            for (int i = 0; i < data.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);

                for (int j = 0; j < properties.Length; j++)
                {
                    var cellValue = properties[j].GetValue(data[i]);
                    CreateTableCell(row, j, cellValue == null ? "" : cellValue.ToString(), style);
                }
            }

            for (int i = 0; i < properties.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream, true);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        private void CreateTableCell(IRow row, int column, string value, ICellStyle style)
        {
            ICell cell = row.CreateCell(column);
            cell.SetCellValue(value);
            cell.CellStyle = style;
        }

        private ICellStyle CreateHeaderStyle(IWorkbook workbook)
        {
            ICellStyle headerStyle = workbook.CreateCellStyle();
            IFont headerFont = workbook.CreateFont();
            headerFont.Color = HSSFColor.White.Index;
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.FillBackgroundColor = HSSFColor.Blue.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Indention = 1;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;

            return headerStyle;
        }

        private ICellStyle CreateCellStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.Color = HSSFColor.Black.Index;
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.Indention = 1;
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            return style;
        }

        public async Task<List<T>> ImportExcelFile<T>(IFormFile excelFile, Func<IRow, List<string>, T> mapFunction)
        {
            var data = new List<T>();
            var headers = new List<string>();

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                var headerRow = sheet.GetRow(0);

                for (int cellIndex = 0; cellIndex < headerRow.LastCellNum; cellIndex++)
                {
                    var cell = headerRow.GetCell(cellIndex);
                    headers.Add(cell.StringCellValue);
                }

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    var excelRow = sheet.GetRow(row);
                    var entity = mapFunction(excelRow, headers);
                    data.Add(entity);
                }
            }

            return data;
        }
    }
}
