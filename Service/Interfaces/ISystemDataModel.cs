namespace Service.Interfaces
{
    public interface ISystemDataModel
    {
        string SortOrder { get; set; }
        string CurrentFilter { get; set; }
        string SearchValue { get; set; }
        int Page { get; set; }
        int ResultsPerPage { get; set; }
        int TotalCount { get; set; }
    }
}