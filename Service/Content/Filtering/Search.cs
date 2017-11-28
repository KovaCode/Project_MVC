using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Content.Filtering
{
    public class Search
    {
       public string SortOrder { get; set; } 
       public string CurrentFilter { get; set; }
        public string SearchValue { get; set; }
    }
}