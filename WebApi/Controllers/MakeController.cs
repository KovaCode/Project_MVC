using Model;
using Model.Common;
using Service.Common.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MakeController : ApiController
    {

        private readonly IVehicleMakeService service;

        public MakeController(IVehicleMakeService service)
        {
            this.service = service;
            Console.WriteLine(System.Guid.NewGuid().ToString());
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> ReadAll()
        {
            IEnumerable<IVehicleMakeModel> allItems = await service.GetVehicleDataAsync();
            return Request.CreateResponse(HttpStatusCode.OK, allItems);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<HttpResponseMessage> Read(Guid id)
        {            
            return Request.CreateResponse(HttpStatusCode.OK, await service.ReadAsync(id));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Create([FromBody]VehicleMakeModel model)
        {
            await service.CreateAsync(model);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task Update([FromBody]VehicleMakeModel model)
        {
            await service.UpdateAsync(model);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await service.DeleteAsync(id);

        }
    }
}