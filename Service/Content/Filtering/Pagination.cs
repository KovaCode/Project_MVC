using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Content.Filtering
{
    public class Pagination
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; } = 5;

    }
}