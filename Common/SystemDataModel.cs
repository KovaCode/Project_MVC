﻿using Service.Common;

namespace Common
{
    public class SystemDataModel : ISystemDataModel
    {
        public bool SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchValue { get; set; }
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 5;
        public int TotalCount { get; set; } = 0;
    }
}