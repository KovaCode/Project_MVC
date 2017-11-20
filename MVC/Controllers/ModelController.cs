﻿using System.Net;
using System.Web.Mvc;
using Service.DAL;
using Service.Models;
using System;
using PagedList;
using MVC.Models;
using System.Collections.Generic;

namespace MVC_Project.Controllers
{
    public class ModelController : Controller
    {

        private ModelService service;
        private AutoMapperProfile autoMapperProfile;


        public ModelController()
        {
            autoMapperProfile = new AutoMapperProfile();
            service = new ModelService(new VehicleDBContext());
        }

        // GET: Models
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            IEnumerable<VehicleModel> modelItems = service.GetModels(sortOrder, currentFilter, search, page);
            IEnumerable<VehicleModelView> makeViewItems = AutoMapperProfile._mapper.Map<IEnumerable<VehicleModelView>>(modelItems);

            int pageNumber = (page ?? 1);
            return View(makeViewItems.ToPagedList(pageNumber, 10));

        }

        // GET: Models/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModel model = service.Read(id);
            VehicleModelView modelView = AutoMapperProfile._mapper.Map<VehicleModelView>(model);          

            if (modelView == null)
            {
                return HttpNotFound();
            }
            return View(modelView);
        }

        // GET: Models/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(service.GetAllMakers(), "Id", "Name");
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MakeID,Name,Abrv")] VehicleModel model)
        {
            if (ModelState.IsValid)
            {
                service.Create(model);
                service.Save();
                return RedirectToAction("Index");
            }
            VehicleModelView modelView = AutoMapperProfile._mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // GET: Models/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel model = service.Read(id);
            VehicleModelView modelView = AutoMapperProfile._mapper.Map<VehicleModelView>(model);
            if (modelView == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeID = new SelectList(service.GetAllMakers(), "Id", "Name");
            return View(modelView);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MakeID,Name,Abrv")] VehicleModel model)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vehicleModel).State = EntityState.Modified;
                //db.SaveChanges();
                service.Update(model);
                service.Save();

                return RedirectToAction("Index");
            }
            VehicleModelView modelView = AutoMapperProfile._mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // GET: Models/Delete/5
        public ActionResult Delete(int? id)
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

            VehicleModelView modelView = AutoMapperProfile._mapper.Map<VehicleModelView>(model);
            return View(modelView);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleModel model = service.Read(id);
            

            service.Delete(id);
            service.Save();
            return RedirectToAction("Index");
        }
    }
}