using ExcelLibrary.SpreadSheet;
using System;

namespace ExcelManager
{
    public static class ExcelLibraryManager
    {

      




        //Only for test
        private static void CreateNewXlsFile()
        {
            //create new xls file
            string file = "D:/newdoc.xls";
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("First Sheet");
            worksheet.Cells[0, 1] = new Cell((short)1);
            worksheet.Cells[2, 0] = new Cell(9999999);
            worksheet.Cells[3, 3] = new Cell((decimal)3.45);
            worksheet.Cells[2, 2] = new Cell("Text string");
            worksheet.Cells[2, 4] = new Cell("Second string");
            worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD");
            worksheet.Cells.ColumnWidth[0, 1] = 3000;
            workbook.Worksheets.Add(worksheet);
            workbook.Save(file);





            // traverse rows by Index for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++) { Row row = sheet.Cells.GetRow(rowIndex); for (int colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++) { Cell cell = row.GetCell(colIndex); } } 
        }

        //Only for test
        private static void OpenXlsFile()
        {
            // open xls file
            string file = "C:\newdoc.xls";
            Workbook book = Workbook.Load(file);
            Worksheet sheet = book.Worksheets[0];

            // traverse cells

        }
    }
}

