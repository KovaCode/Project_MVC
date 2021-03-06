﻿namespace Service.Common
{
    public interface ISystemDataModel
    {
        bool SortOrder { get; set; }
        string CurrentFilter { get; set; }
        string SearchValue { get; set; }
        int Page { get; set; }
        int ResultsPerPage { get; set; }
        int TotalCount { get; set; }
    }
}