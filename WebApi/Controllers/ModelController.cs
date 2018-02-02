using AutoMapper;
using Model;
using Model.Common;
using Service.Common.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ModelController : ApiController
    {
        private readonly IVehicleModelService service;

        public ModelController(IVehicleModelService service)
        {
            this.service = service;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> ReadAll()
        {
            IEnumerable<ModelRestModel> allItems = Mapper.Map<IEnumerable<ModelRestModel>>(await service.GetVehicleDataAsync());
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
        public async Task Create([FromBody]ModelRestModel model)
        {
            await service.CreateAsync(Mapper.Map<VehicleModelModel>(model));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task Update([FromBody]ModelRestModel model)
        {
            await service.UpdateAsync(Mapper.Map<VehicleModelModel>(model));
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await service.DeleteAsync(id);

        }
    }
}