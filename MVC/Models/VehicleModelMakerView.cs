using PagedList;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class VehicleModelMakerView
    {
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchValue { get; set; }
        public int? Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 5;
        public IPagedList<VehicleModelView> Models { get; set; }
        public IEnumerable<VehicleMake> AllMakes { get; set;  }
    }
}