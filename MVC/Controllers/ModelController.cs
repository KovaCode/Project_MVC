﻿using System.Net;
using System.Web.Mvc;
using Service.Models;
using System;
using PagedList;
using MVC.Models;
using System.Collections.Generic;
using AutoMapper;
using Service.Services;
using Service.Common.Services;
using Model.Common;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class ModelController : Controller
    {
        private IVehicleModelService service;

        public ModelController(IVehicleModelService service)
        {
            this.service = service;
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

            StaticPagedList<IVehicleModelModel> modelItems = service.GetVehicleDataPaged(systemDataModel);
            StaticPagedList<VehicleModelView> modelViewPaged = Mapper.Map<StaticPagedList<IVehicleModelModel>, StaticPagedList<VehicleModelView>>(modelItems);
            ViewBag.CurrentFilter = systemDataModel.SearchValue;

            return View(modelViewPaged);
        }

        // GET: Models/Details/5
        public async Task<ActionResult> DetailsAsync(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IVehicleModelModel model = await service.ReadAsync(id);
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
                MakeEnumerable = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<VehicleMakeView>>(service.GetMakes())
            };
            return View(vehicleModelView);
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id, Make ,VehicleMakeId, Make.Id, MakeID,Name,Abrv")] VehicleModelView modelView)
        {

            IVehicleModelModel model = Mapper.Map<IVehicleModelModel>(modelView);
            if (ModelState.IsValid)
            {
                await service.CreateAsync(model);
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

            VehicleModelView modelView = Mapper.Map<VehicleModelView>(service.ReadAsync(id));
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
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,VehicleMakeId,Name,Abrv")] VehicleModelView modelView)
        {
            IVehicleModelModel model = Mapper.Map<IVehicleModelModel>(modelView);

            if (ModelState.IsValid)
            {
                await service.UpdateAsync(model);

                return RedirectToAction("Index");
            }
            //VehicleModelView modelView = Mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // GET: Models/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleModelModel model = await service.ReadAsync(id);
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
        public async Task<ActionResult> DeleteConfirmedAsync(Guid? id)
        {
            IVehicleModelModel model = await service.ReadAsync(id);
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
