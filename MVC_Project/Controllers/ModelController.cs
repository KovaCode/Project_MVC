using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC_Project.DAL;
using MVC_Project.Models;
using AutoMapper;

namespace MVC_Project.Controllers
{
    public class ModelController : Controller
    {
        private ModelService service;
        //private VehicleDBContext db = new VehicleDBContext();

        public ModelController()
        {
            service = new ModelService(new VehicleDBContext());
        }

        // GET: Models
        public ActionResult Index()
        {
            //AutoMapper.Mapper.Map<>;
            var model = service.getModelsQueryable().Include(e => e.Makers);

            //Mapper.CreateMap<Maker, Models.Maker>();
            //Mapper.CreateMap<Address, Models.AddressModel>();
            //List<Models.ContactModel> theList = Mapper.Map<List<Contact>, List<Models.ContactModel>>(contacts);


            return View(model);

        }

        // GET: Models/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model vehicleModel = service.Read(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: Models/Create
        public ActionResult Create()
        {
            ViewBag.MakeID = new SelectList(service.getModels(), "Id", "Name");
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MakeID,Name,Abrv")] Model model)
        {
            if (ModelState.IsValid)
            {
                service.Create(model);
                service.Save(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Models/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model vehicleModel = service.Read(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeID = new SelectList(db.VehicleMake, "Id", "Name");
            return View(vehicleModel);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MakeId,Name,Abrv")] Model model)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vehicleModel).State = EntityState.Modified;
                //db.SaveChanges();
                service.Create(model);
                service.Save();

                return RedirectToAction("Index");
            }
            return View(vehicleModel);
        }

        // GET: Models/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model vehicleModel = service.Read(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Model model = service.Read(id);
            service.Delete(id);
            service.Save(model);
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
