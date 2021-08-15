using ExcelServices.Interfaces;
using ExcelServices.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelServices
{
    public class NPOIService : IExcelService
    {
        private HSSFWorkbook _workbook;
        private ICellStyle _cellStyle;
        public void Create(string path, Workbook workbook)
        {
            _cellStyle = null;
            _workbook = new HSSFWorkbook();
            foreach (var worksheet in workbook.Worksheets)
            {
                var ws = _workbook.CreateSheet(worksheet.Name);
                int actualRowIndex = -1;
                IRow row = null;
                foreach (var cell in worksheet.Cells.OrderBy(c => c.X))
                {
                    if (cell.X > actualRowIndex)
                    {
                        row = ws.CreateRow(cell.X);
                        actualRowIndex = cell.X;
                    }
                    CreateCell(row, cell);
                }
                SetColumnsSize(ws, worksheet.ColumnsSize);
            }



            using (var stream = new FileStream(path + workbook.Name + ".xls", FileMode.Create, FileAccess.Write))
            {
                _workbook.Write(stream);
            }
        }

        private void CreateCell(IRow currentRow, Cell cell)
        {
            //Base : https://dev.to/mtmb/generate-excel-with-npoi-in-c-904
            ICell npoiCell = currentRow.CreateCell(cell.Y);
            npoiCell.SetCellValue(cell.Value);

            if (cell.CellStyle != null)
            {
                IFont font = _workbook.CreateFont();
                font.IsBold = cell.CellStyle.IsBold;
                font.IsItalic = cell.CellStyle.IsItalic;
                if (_cellStyle == null)
                    _cellStyle = _workbook.CreateCellStyle();
                _cellStyle.SetFont(font);
                switch (cell.CellStyle.HorizontalAlignment)
                {
                    case EHorizontalAlignment.General:
                        _cellStyle.Alignment = HorizontalAlignment.General;
                        break;
                    case EHorizontalAlignment.Left:
                        _cellStyle.Alignment = HorizontalAlignment.Left;
                        break;
                    case EHorizontalAlignment.Center:
                        _cellStyle.Alignment = HorizontalAlignment.Center;
                        break;
                    case EHorizontalAlignment.Right:
                        _cellStyle.Alignment = HorizontalAlignment.Right;
                        break;
                    case EHorizontalAlignment.Fill:
                        _cellStyle.Alignment = HorizontalAlignment.Fill;
                        break;
                    case EHorizontalAlignment.Justify:
                        _cellStyle.Alignment = HorizontalAlignment.Justify;
                        break;
                    case EHorizontalAlignment.CenterSelection:
                        _cellStyle.Alignment = HorizontalAlignment.CenterSelection;
                        break;
                    case EHorizontalAlignment.Distributed:
                        _cellStyle.Alignment = HorizontalAlignment.Distributed;
                        break;
                    default:
                        break;
                }
                switch (cell.CellStyle.VerticalAlignment)
                {
                    case EVerticalAlignment.None:
                        _cellStyle.VerticalAlignment = VerticalAlignment.None;
                        break;
                    case EVerticalAlignment.Top:
                        _cellStyle.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case EVerticalAlignment.Center:
                        _cellStyle.VerticalAlignment = VerticalAlignment.Center;
                        break;
                    case EVerticalAlignment.Bottom:
                        _cellStyle.VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                    case EVerticalAlignment.Justify:
                        _cellStyle.VerticalAlignment = VerticalAlignment.Justify;
                        break;
                    case EVerticalAlignment.Distributed:
                        _cellStyle.VerticalAlignment = VerticalAlignment.Distributed;
                        break;
                    default:
                        break;
                }
                npoiCell.CellStyle = _cellStyle;
            }
        }

        private void SetColumnsSize(ISheet sheet, List<ColumnSize> columnsSize)
        {
            if (columnsSize == null)
                return;
            foreach (var columnSize in columnsSize)
            {
                if (columnSize.Size == ColumnSize.AUTOSIZE)
                    sheet.AutoSizeColumn(columnSize.Index);
                else
                    sheet.SetColumnWidth(columnSize.Index, columnSize.Size * 256);
            }
        }
    }
}
