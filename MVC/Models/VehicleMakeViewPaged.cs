
using PagedList;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVC.Models
{
    
    public class VehicleMakeViewPaged
    {
        public IPagedList<VehicleMakeView> MakePaged { get; set; }
    }
}