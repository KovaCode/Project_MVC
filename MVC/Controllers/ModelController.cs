using System.Net;
using System.Web.Mvc;
using Service.DAL;
using Service.Models;
using System;
using PagedList;
using MVC.Models;
using System.Collections.Generic;
using System.Collections;
using AutoMapper;

namespace MVC_Project.Controllers
{
    public class ModelController : Controller
    {
        private MainView mainView = new MainView();
        private SystemDataModel systemDataModel = new SystemDataModel();
        private ModelService service;

        public ModelController()
        {
            service = new ModelService();
        }

        // GET: Models
        public ActionResult Index(string sortOrder, string currentFilter, string searchValue, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            systemDataModel.SortOrder = sortOrder;
            systemDataModel.SearchValue = searchValue;
            mainView.MakerEnumerable = service.GetAllMakers();

            IEnumerable<VehicleModel> modelItems = service.GetModels(systemDataModel);
            IEnumerable <VehicleMakeView> makeView = Mapper.Map<IEnumerable<VehicleMakeView>>(mainView.MakerEnumerable);
            IEnumerable<VehicleModelView> modelView = Mapper.Map<IEnumerable<VehicleModelView>>(modelItems);
            
            int pageNumber = (page ?? 1);
            mainView.ModelPaged = modelView.ToPagedList(pageNumber, systemDataModel.ResultsPerPage);
            return View(mainView);
        }

        // GET: Models/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModel model = service.Read(id);
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
            mainView.MakerEnumerable = service.GetAllMakers();
            ViewBag.MakerList = mainView.ListMakers;

            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleMakeId, MakeID,Name,Abrv")] VehicleModel model)
        {
            if (ModelState.IsValid)
            {
                service.Create(model);
                return RedirectToAction("Index");
            }
            VehicleModelView modelView = Mapper.Map<VehicleModelView>(model);

            ViewBag.MakerList = mainView.ListMakers;

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
            ViewBag.MakerList = new SelectList(service.GetAllMakers(), "Id", "Name", modelView.VehicleMakeId);
            return View(modelView);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleMakeId,Name,Abrv")] VehicleModel model)
        {
            if (ModelState.IsValid)
            {
                service.Update(model);

                return RedirectToAction("Index");
            }
            VehicleModelView modelView = Mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // GET: Models/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel model = service.Read(id);
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
            VehicleModel model = service.Read(id);
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
