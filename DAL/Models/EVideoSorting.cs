namespace DAL.Models
{
    public enum EVideoSorting : int
    {
        NotSorted,
        DurationAsc,
        DurationDesc,
        DateCreationAsc,
        DateCreationDesc,
        NumberViewsAsc,
        NumberViewsDesc,
        NumberPositivesFeedbackAsc,
        NumberPositivesFeedbackDesc,
        NumberNegativesFeedbackAsc,
        NumberNegativesFeedbackDesc,
        NameAsc,
        NameDesc
    }
}