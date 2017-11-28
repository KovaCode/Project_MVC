using PagedList;
using Service.Models;
using System.Collections.Generic;



namespace Service.Models
{
    public class SystemDataModel
    {
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchValue { get; set; }
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 5;
    }
}