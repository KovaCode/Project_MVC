using System.Net;
using System.Web.Mvc;
using System;
using PagedList;
using MVC.Models;
using System.Collections.Generic;
using AutoMapper;
using Service.Common.Services;
using Model.Common;
using System.Threading.Tasks;
using Common;
using System.Collections;

namespace MVC_Project.Controllers
{
    public class ModelController : Controller
    {
        private IVehicleModelService service;

        public ModelController(IVehicleModelService service)
        {
            this.service = service;
        }

        // GET: Models
        public async Task<ActionResult> Index(bool sortOrder, string currentFilter, string searchString, int? page, int? resultsPerPage)
        {
            SystemDataModel systemDataModel = new SystemDataModel();

            ViewBag.ResultsPerPage = resultsPerPage;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm =  !sortOrder;

            systemDataModel.SearchValue = searchString;
            systemDataModel.CurrentFilter = currentFilter;
            systemDataModel.SortOrder = sortOrder;
            systemDataModel.ResultsPerPage = (resultsPerPage ?? 5);
            systemDataModel.Page = (page ?? 1);

            StaticPagedList<IVehicleModelModel> items = await service.GetVehicleDataPagedAsync(systemDataModel);
            StaticPagedList<VehicleModelView> modelViewPaged = Mapper.Map<StaticPagedList<IVehicleModelModel>, StaticPagedList<VehicleModelView>>(items);
            ViewBag.CurrentFilter = systemDataModel.SearchValue;

            return View(modelViewPaged);
        }

        // GET: Models/Details/5
        public async Task<ActionResult> Details(Guid? id)
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
        public async Task<ActionResult> Create()
        {
            VehicleModelView vehicleModelView = new VehicleModelView
            {
                MakeEnumerable = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<VehicleMakeView>>(await service.GetAllMakeAsync())
            };
            return View(vehicleModelView);
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id, Make ,VehicleMakeId, Make.Id, MakeID,Name,Abrv")] VehicleModelView modelView)
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
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModelView modelView = Mapper.Map<VehicleModelView>(await service.ReadAsync(id));
            if (modelView == null)
            {
                return HttpNotFound();
            }
            //IEnumerable<VehicleMakeView> modelViewList = Mapper.Map<IEnumerable<VehicleModelView>(service.GetMakesAsync());

            //ViewBag.MakerList = new SelectList(), "Id", "Name");
            return View(modelView);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VehicleMakeId,Name,Abrv")] VehicleModelView modelView)
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
        public async Task<ActionResult> Delete(Guid? id)
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
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            IVehicleModelModel model = await service.ReadAsync(id);
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
