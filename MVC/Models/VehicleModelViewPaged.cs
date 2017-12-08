
using PagedList;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Models
{
    
    public class VehicleModelViewPaged
    {
        public IEnumerable<IVehicleMake> MakerEnumerable;
        
        public int SelectedMaker { get; set; }

        public IEnumerable<SelectListItem> ListMakers
        {
            get { return new SelectList(MakerEnumerable, "Id", "Name"); }
        }
        public IPagedList<VehicleModelView> ModelPaged { get; set; }
    }
}