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
        private AutoMapperProfile autoMapperProfile = new AutoMapperProfile();
        public MakeController()
        {
            service = new MakerService();
        }

        // GET: /Maker/
        public ViewResult Index(string searchValue, string sortOrder, string currentSort, int? page)
        {
            VehicleMakeViewPaged vehicleMakeViewPaged = new VehicleMakeViewPaged();
            SystemDataModel systemData = new SystemDataModel();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            systemData.SearchValue = searchValue;
            systemData.SortOrder = sortOrder;

            IEnumerable<VehicleMake> makeItems = service.GetMakers(systemData);
            IEnumerable<VehicleMakeView> makeViewItems = Mapper.Map<IEnumerable<VehicleMakeView>>(makeItems);            
            systemData.Page = (page ?? 1);

            vehicleMakeViewPaged.MakePaged = makeViewItems.ToPagedList(systemData.Page, systemData.ResultsPerPage);
            return View(vehicleMakeViewPaged);
        }
        
        // GET: /Maker/Details/5
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

        // GET: /Maker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Maker/Create
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

        // GET: /Maker/Edit/5
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

        // POST: /Maker/Edit/5
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
