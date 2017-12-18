using System.Net;
using System.Web.Mvc;
using Service.Models;
using System;
using PagedList;
using MVC.Models;
using System.Collections.Generic;
using AutoMapper;
using Service.Services;
using Service.Interfaces.Services;
using Service.Interfaces.Models;

namespace MVC_Project.Controllers
{
    public class ModelController : Controller
    {
        private IVehicleModelService service;

        public ModelController()
        {
            service = new VehicleModelService();
        }

        public ModelController(VehicleModelService vehicleModelService)
        {
            service = vehicleModelService;
        }

        // GET: Models
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? resultsPerPage)
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

            StaticPagedList<IVehicleModel> modelItems = service.GetVehicleDataPaged(systemDataModel);
            StaticPagedList<VehicleModelView> modelViewPaged = Mapper.Map<StaticPagedList<IVehicleModel>, StaticPagedList<VehicleModelView>>(modelItems);
            ViewBag.CurrentFilter = systemDataModel.SearchValue;

            return View(modelViewPaged);
        }

        // GET: Models/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IVehicleModel model = service.Read(id);
            VehicleModelView modelView = Mapper.Map<VehicleModelView>(model);
            

            if (modelView == null)
            {
                return HttpNotFound();
            }
            return View(modelView);
        }

        // GET: Models/Create
        public ActionResult Create()
        {
            VehicleModelView vehicleModelView = new VehicleModelView
            {
                MakeEnumerable = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeView>>(service.GetMakes())
            };
            return View(vehicleModelView);
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Make ,VehicleMakeId, Make.Id, MakeID,Name,Abrv")] VehicleModelView modelView)
        {

            IVehicleModel model = Mapper.Map<IVehicleModel>(modelView);
            if (ModelState.IsValid)
            {
                service.Create(model);
                return RedirectToAction("Index");
            }
            modelView = Mapper.Map<VehicleModelView>(model);
            return View(modelView);

        }

        // GET: Models/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModelView modelView = Mapper.Map<VehicleModelView>(service.Read(id));
            if (modelView == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakerList = new SelectList(service.GetMakes(), "Id", "Name");
            return View(modelView);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleMakeId,Name,Abrv")] VehicleModelView modelView)
        {
            IVehicleModel model = Mapper.Map<IVehicleModel>(modelView);

            if (ModelState.IsValid)
            {
                service.Update(model);

                return RedirectToAction("Index");
            }
            //VehicleModelView modelView = Mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // GET: Models/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleModel model = service.Read(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            VehicleModelView modelView = Mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            IVehicleModel model = service.Read(id);
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
