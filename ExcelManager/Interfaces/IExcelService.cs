using ExcelServices.Models;

namespace ExcelServices.Interfaces
{
    public interface IExcelService
    {
        public void Create(string path, Workbook workbook);
    }
}
