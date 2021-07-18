namespace ExcelServices.Models
{
    public class CellStyle
    {
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }
        public EHorizontalAlignment HorizontalAlignment { get; set; }
        public EVerticalAlignment VerticalAlignment { get; set; }
    }
}
