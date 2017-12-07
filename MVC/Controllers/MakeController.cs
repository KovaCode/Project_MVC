using System;
using System.Web.Mvc;
using Service.DAL;
using System.Net;
using Service.Models;
using MVC.Models;
using System.Collections.Generic;
using PagedList;
using AutoMapper;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {
        private MakerService service;

        public MakeController()
        {
            service = new MakerService();
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

            //IPagedList<VehicleMakeView> makeViewItems = Mapper.Map<IPagedList<VehicleMakeView>>(service.GetVehicleDataPaged(systemDataModel));
            IPagedList<VehicleMakeView> makeViewItems = Mapper.Map<PagedList<VehicleMakeView>>(service.GetVehicleDataPaged(systemDataModel));
            //VehicleMakeView makeViewItems = Mapper.Map<VehicleMakeView>(service.GetVehicleDataPaged(systemDataModel));
            ViewBag.CurrentFilter = systemDataModel.SearchValue;

            //vehicleMakeViewPaged.MakePaged = makeViewItems.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
            //return View(service.GetVehicleDataPaged());

            return View(makeViewItems);
        }
        
        // GET: /Make/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = service.Read(id);
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
        public ActionResult Create([Bind(Include = "Id,Name,Abrv")] VehicleMake make)
        {
            if (ModelState.IsValid)
            {
                service.Create(make);
                return RedirectToAction("Index");
            }
            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);
            return View(makeView);
        }

        // GET: /Make/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = service.Read(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Abrv")] VehicleMake make)
        {
            if (ModelState.IsValid)
            {
                service.Update(make);
                return RedirectToAction("Index");
            }

            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);
            return View(makeView);
        }

        // GET: /Make/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = service.Read(id);
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
            VehicleMake maker = service.Read(id);
            service.Delete(id);
            return RedirectToAction("Index");
        }

  
    }
}
