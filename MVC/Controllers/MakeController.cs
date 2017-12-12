using System;
using System.Web.Mvc;
using System.Net;
using Service.Models;
using Service.Services;
using MVC.Models;
using PagedList;
using AutoMapper;
using Service.Interfaces;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {
        private VehicleMakeService service;

        public MakeController()
        {
            service = new VehicleMakeService();
        }

        // GET: /Make/
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            SystemDataModel systemDataModel = new SystemDataModel();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";

            systemDataModel.SearchValue = searchString;
            systemDataModel.CurrentFilter = currentFilter;
            systemDataModel.SortOrder = sortOrder;
            systemDataModel.Page = (page ?? 1);

            IPagedList<IVehicleMake> makeItems = service.GetVehicleDataPaged(systemDataModel);
            IPagedList<VehicleMakeView> makeViewItems = Mapper.Map<IPagedList<IVehicleMake>, IPagedList<VehicleMakeView>>(makeItems);

           ViewBag.CurrentFilter = systemDataModel.SearchValue;
            return View(makeItems);
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
            VehicleMake make = Mapper.Map<VehicleMake>(makeView);

            if (ModelState.IsValid)
            {
                service.Create(make);
                return RedirectToAction("Index");
            }
            //VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);
            return View(makeView);
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
            VehicleMake make = Mapper.Map<VehicleMake>(makeView);

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
