using Model.Common;
using Newtonsoft.Json;
using Service.Common.Services;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

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
        public async Task<HttpResponseMessage> Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await service.GetVehicleDataAsync());

        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> Get(Guid id)
        {            
            return Request.CreateResponse(HttpStatusCode.OK, await service.ReadAsync(id));

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}