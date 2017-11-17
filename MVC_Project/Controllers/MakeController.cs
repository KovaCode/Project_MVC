using System;
using System.Web.Mvc;
using Service.DAL;
using System.Net;
using Service.Models;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {
        private MakerService service;

        public MakeController()
        {
            service = new MakerService(new VehicleDBContext());
        }


        // GET: /Maker/
        public ViewResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            var makeItems = service.getMakers(sortOrder, currentFilter, search, page);

            //var makeItems = service.getMakers(sortOrder, currentFilter, search, page);

            //if (!String.IsNullOrEmpty(search))
            //{
            //    makeItems = makeItems.Where(s => s.Name.Contains(search) || s.Abrv.Contains(search));
            //}

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        makeItems = makeItems.OrderBy(s => s.Name);
            //        break;
            //    case "name_asc":
            //        makeItems = makeItems.OrderBy(s => s.Name);
            //        break;

            //    case "Abrv":
            //        makeItems = makeItems.OrderBy(s => s.Abrv);
            //        break;
            //    default:
            //        makeItems = makeItems.OrderBy(s => s.Name);
            //        break;
            //}

            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            return View(makeItems/*.ToPagedList(pageNumber, pageSize)*/);
        }

        // GET: /Maker/Details/5
        public ActionResult Details(int id)
        {
            if (id <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            VechicleMake make = service.Read(id);


            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
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
        public ActionResult Create([Bind(Include = "Id,Name,Abrv")] VechicleMake maker)
        {
            if (ModelState.IsValid)
            {
                service.Create(maker);
                service.Save();

                return RedirectToAction("Index");
            }
            return View(maker);
        }

        // GET: /Maker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VechicleMake make = service.Read(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // POST: /Maker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Abrv")] VechicleMake maker)
        {
            if (ModelState.IsValid)
            {
                service.Update(maker);
                service.Save();

                return RedirectToAction("Index");
            }
            return View(maker);
        }

        // GET: /Make/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VechicleMake maker = service.Read(id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // POST: /Make/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VechicleMake maker = service.Read(id);
            service.Delete(id);
            service.Save();
            return RedirectToAction("Index");
        }

  
    }
}
