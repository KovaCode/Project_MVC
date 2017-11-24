using System;
using System.Web.Mvc;
using Service.DAL;
using System.Net;
using Service.Models;
using MVC.Models;
using System.Collections.Generic;
using PagedList;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {
        //private Pagination pagination = new Pagination();
        private MakerService service;
        private AutoMapperProfile autoMapperProfile;

        public MakeController()
        {
            autoMapperProfile = new AutoMapperProfile();
            service = new MakerService();
        }

        // GET: /Maker/
        public ViewResult Index(string searchValue, string sortOrder, string currentSort, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            IEnumerable<VehicleMake> makeItems = service.GetMakers(currentSort, sortOrder, searchValue, page);
            IEnumerable<VehicleMakeView> makeViewItems = AutoMapperProfile._mapper.Map<IEnumerable<VehicleMakeView>>(makeItems);

            int pageNumber = (page ?? 1);
            //pagination.Page = (page ?? 1);
            //return View(makeViewItems.ToPagedList(pagination.Page, pagination.ResultsPerPage));
            return View(makeViewItems.ToPagedList(pageNumber, 5));
        }
        
        // GET: /Maker/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleMake make = service.Read(id);
            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);

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
            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);
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
            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);
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

            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);
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
            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);
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
