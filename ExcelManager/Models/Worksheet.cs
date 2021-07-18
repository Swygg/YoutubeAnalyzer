using System.Collections.Generic;

namespace ExcelServices.Models
{
    public class Worksheet
    {
        public string Name { get; set; }
        public List<Cell> Cells { get; set; }
        public List<ColumnSize> ColumnsSize { get; set; }

        public Worksheet()
        {
            this.Cells = new List<Cell>();
        }

        public Worksheet(string Name) : this()
        {
            this.Name = Name;
        }

    }
}
