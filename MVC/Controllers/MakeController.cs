using System;
using System.Web.Mvc;
using System.Net;
using Service.Models;
using MVC.Models;
using PagedList;
using AutoMapper;
using System.Linq;
using Service.Common.Services;
using Model.Common;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class MakeController : Controller
    {      
        private readonly IVehicleMakeService service;

        public MakeController(IVehicleMakeService vehicleMakeService)
        {
            service = vehicleMakeService;
        }

        // GET: /Make/
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? resultsPerPage)
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


            StaticPagedList<IVehicleMakeModel> makeItems = service.GetVehicleDataPaged(systemDataModel);
            StaticPagedList<VehicleMakeView> makeViewItems = Mapper.Map<StaticPagedList<IVehicleMakeModel>, StaticPagedList<VehicleMakeView>>(makeItems);
                
           ViewBag.CurrentFilter = systemDataModel.SearchValue;
            return View(makeViewItems);
        }
        
        // GET: /Make/Details/5
        public async Task<ActionResult> DetailsAsync(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMakeModel make = await service.ReadAsync(id);
            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);

            if (makeView == null)
            {
                return HttpNotFound();
            }
            return View(makeView);
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
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,Name,Abrv")] VehicleMakeView makeView)
        {
            IVehicleMakeModel make = Mapper.Map<IVehicleMakeModel>(makeView);

            if (ModelState.IsValid)
            {
                await service.CreateAsync(make);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Something went wrong";
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                return View(makeView);

            }
        }
        
        // GET: /Make/Edit/5
        public async Task<ActionResult> EditAsync(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMakeModel make = await service.ReadAsync(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            VehicleMakeView makeView = Mapper.Map<VehicleMakeView>(make);
            return View(makeView);
        }

        // POST: /Make/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,Name,Abrv")] VehicleMakeView makeView)
        {
            IVehicleMakeModel make = Mapper.Map<IVehicleMakeModel>(makeView);

            if (ModelState.IsValid)
            {
                await service.UpdateAsync(make);
                return RedirectToAction("Index");
            }

            
            return View(makeView);
        }

        // GET: /Make/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMakeModel make = await service.ReadAsync(id);
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
        public async Task<ActionResult> DeleteConfirmedAsync(Guid? id)
        {
            IVehicleMakeModel maker = await service.ReadAsync(id);
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

  
    }
}
