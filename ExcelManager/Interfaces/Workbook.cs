using System.Collections.Generic;

namespace ExcelServices.Interfaces
{
    public class Workbook
    {
        public string Name { get; set; }
        public List<Worksheet> Worksheets { get; set; }

        public Workbook()
        {
            Worksheets = new List<Worksheet>();
        }

        public Workbook(string Name) : this()
        {
            this.Name = Name;
        }
    }
}
