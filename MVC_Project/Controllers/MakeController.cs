using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project.DAL;
using MVC_Project.Models;
using System.Net;
using System.Data.Entity;
using AutoMapper;

namespace MVC_Project.Controllers
{
    public class MakeController : Controller
    {
        private VehicleDBContext db = new VehicleDBContext();

        // GET: /Make/
        public ActionResult Index()
        {
            //var model = db.VehicleMake.Include(e => e.Id);
            //return View(model);
            return View(db.VehicleMake.ToList());
        }

        // GET: /Make/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = db.VehicleMake.Find(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
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
                db.VehicleMake.Add(make);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(make);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = db.VehicleMake.Find(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
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
                db.Entry(make).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(make);
        }

        // GET: /Make/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = db.VehicleMake.Find(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // POST: /Make/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleMake make = db.VehicleMake.Find(id);
            db.VehicleMake.Remove(make);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
