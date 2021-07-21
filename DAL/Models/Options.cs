namespace DAL.Models
{
    public class Options
    {
        public string DateFormat { get; set; }
        public EDurationFormat DurationFormat { get; set; }
        public string ThousandSeparator { get; set; }
        public string MillionsSeparator { get; set; }
        public string BilliardSeparator { get; set; }

        public Options()
        {
            ThousandSeparator = string.Empty;
            MillionsSeparator = string.Empty;
            BilliardSeparator = string.Empty;
        }
    }
}
