using System;
using System.Web.Mvc;
using System.Net;
using Service.Models;
using MVC.Models;
using PagedList;
using AutoMapper;
using Service.Servicess;
using System.Linq;
using Service.Interfaces.Services;
using Service.Interfaces.Models;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {
        private IVehicleMakeService service;

        public MakeController()
        {
            service = new VehicleMakeService();
        }

        public MakeController(VehicleMakeService vehicleMakeService)
        {
            service = vehicleMakeService;
        }


        // GET: /Make/
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? resultsPerPage)
        {
            SystemDataModel systemDataModel = new SystemDataModel();

            ViewBag.ResultsPerPage = resultsPerPage;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";

            systemDataModel.SearchValue = searchString;
            systemDataModel.CurrentFilter = currentFilter;
            systemDataModel.SortOrder = sortOrder;
            systemDataModel.ResultsPerPage = (resultsPerPage ?? 5);
            systemDataModel.Page = (page ?? 1);


            StaticPagedList<IVehicleMake> makeItems = service.GetVehicleDataPaged(systemDataModel);
            StaticPagedList<VehicleMakeView> makeViewItems = Mapper.Map<StaticPagedList<IVehicleMake>, StaticPagedList<VehicleMakeView>>(makeItems);
                
           ViewBag.CurrentFilter = systemDataModel.SearchValue;
            return View(makeViewItems);
        }
        
        // GET: /Make/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMake make = service.Read(id);
            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);

            if (makeView == null)
            {
                return HttpNotFound();
            }
            return View(makeView);
        }

        // GET: /Make/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Make/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Abrv")] VehicleMakeView makeView)
        {
            IVehicleMake make = Mapper.Map<IVehicleMake>(makeView);

            if (ModelState.IsValid)
            {
                service.Create(make);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Something went wrong";
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                return View(makeView);

            }
        }
        
        // GET: /Make/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMake make = service.Read(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);
            return View(makeView);
        }

        // POST: /Make/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Abrv")] VehicleMakeView makeView)
        {
            IVehicleMake make = Mapper.Map<IVehicleMake>(makeView);

            if (ModelState.IsValid)
            {
                service.Update(make);
                return RedirectToAction("Index");
            }

            
            return View(makeView);
        }

        // GET: /Make/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMake make = service.Read(id);
            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);
            if (makeView == null)
            {
                return HttpNotFound();
            }
           
            return View(makeView);
        }

        // POST: /Make/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            IVehicleMake maker = service.Read(id);
            service.Delete(id);
            return RedirectToAction("Index");
        }

  
    }
}
