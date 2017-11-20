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
        private const int pageSize = 10;
        private MakerService service;
        private AutoMapperProfile autoMapperProfile;

        public MakeController()
        {
            autoMapperProfile = new AutoMapperProfile();
            service = new MakerService(new VehicleDBContext());
        }


        // GET: /Maker/
        public ViewResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            IEnumerable<VehicleMake> makeItems = service.GetMakers(sortOrder, currentFilter, search, page);

            IEnumerable<VehicleMakeView> makeViewItems = AutoMapperProfile._mapper.Map<IEnumerable<VehicleMakeView>>(makeItems);

            
            int pageNumber = (page ?? 1);
            return View(makeViewItems.ToPagedList(pageNumber,10));
        }
        
        // GET: /Maker/Details/5
        public ActionResult Details(int id)
        {
            if (id <=0)
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
                service.Save();

                return RedirectToAction("Index");
            }
            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);
            return View(makeView);
        }

        // GET: /Maker/Edit/5
        public ActionResult Edit(int? id)
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
                service.Save();

                return RedirectToAction("Index");
            }

            VehicleMakeView makeView = AutoMapperProfile._mapper.Map<VehicleMakeView>(make);
            return View(makeView);
        }

        // GET: /Make/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleMake maker = service.Read(id);
            service.Delete(id);
            service.Save();
            return RedirectToAction("Index");
        }

  
    }
}
