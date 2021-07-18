namespace ExcelServices.Interfaces
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Value { get; set; }
        public CellStyle CellStyle { get; set; }

        public Cell(int x, int y, object value)
        {
            this.X = x;
            this.Y = y;
            if (value == null)
                value = "/";
            this.Value = value.ToString();
        }

        public Cell(int x, int y, object value, CellStyle cellStyle) : this(x, y, value)
        {
            this.CellStyle = cellStyle;
        }
    }
}
