using ExcelServices.Interfaces;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Linq;

namespace ExcelServices
{
    public class NPOIService : IExcelService
    {
        public void Create(string path, Workbook workbook)
        {
            HSSFWorkbook wb = new HSSFWorkbook();
            foreach (var worksheet in workbook.Worksheets)
            {
                var ws = wb.CreateSheet(worksheet.Name);
                int actualRowIndex = -1;
                IRow row = null;
                foreach (var cell in worksheet.Cells.OrderBy(c=> c.X))
                {
                    if (cell.X > actualRowIndex)
                    {
                        row = ws.CreateRow(cell.X);
                        actualRowIndex = cell.X;
                    }
                    CreateCell(row, cell.Y, cell.Value, null);
                }
            }

            using (var stream = new FileStream(path + workbook.Name + ".xls", FileMode.Create, FileAccess.Write))
            {
                wb.Write(stream);
            }
        }

        //Thanks to : https://dev.to/mtmb/generate-excel-with-npoi-in-c-904
        private void CreateCell(IRow currentRow, int cellIndex, string value, HSSFCellStyle style)
        {
            ICell Cell = currentRow.CreateCell(cellIndex);
            Cell.SetCellValue(value);
            if (style != null)
                Cell.CellStyle = style;
        }
    }
}
