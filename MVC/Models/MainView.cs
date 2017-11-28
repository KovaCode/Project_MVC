
using PagedList;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVC.Models
{
    
    public class MainView
    {
        public IEnumerable<VehicleMake> MakerEnumerable;
        
        public int selectedMaker { get; set; }

        public IEnumerable<SelectListItem> ListMakers
        {
            get { return new SelectList(MakerEnumerable, "Id", "Name"); }
        }


        public IPagedList<VehicleMakeView> MakePaged { get; set; }
        public IPagedList<VehicleModelView> ModelPaged { get; set; }
    }
}