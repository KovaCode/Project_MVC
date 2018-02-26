﻿using AutoMapper;
using Common;
using Model;
using Model.Common;
using Service.Common;
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
    [RoutePrefix("api/make")]
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
        [Route("")]
        public async Task<HttpResponseMessage> Read()
        {
            IEnumerable<MakeRestModel> allItems = Mapper.Map<IEnumerable<MakeRestModel>>(await service.GetVehicleDataAsync());
            return Request.CreateResponse(HttpStatusCode.OK, allItems);
        }

        [HttpGet]
        [Route("find")]
        public async Task<HttpResponseMessage> Find([FromUri]SystemDataModel model)
        {
            IEnumerable<MakeRestModel> allItems = Mapper.Map<IEnumerable<MakeRestModel>>(await service.GetVehicleDataAsync(model));
            return Request.CreateResponse(HttpStatusCode.OK, allItems);
        }


        //[HttpGet]
        //[Route("find")]
        //public async Task<HttpResponseMessage> Find(ISystemDataModel model)
        //{
        //    IEnumerable<MakeRestModel> allItems = Mapper.Map<IEnumerable<MakeRestModel>>(await service.GetVehicleDataAsync(model));
        //    return Request.CreateResponse(HttpStatusCode.OK, allItems);
        //}


        // GET api/<controller>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<HttpResponseMessage> Read(Guid id)
        {            
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<MakeRestModel> (await service.ReadAsync(id)));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Create([FromBody]MakeRestModel model)
        {
            await service.CreateAsync(Mapper.Map<VehicleMakeModel>(model));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task Update([FromBody]MakeRestModel model)
        {
            await service.UpdateAsync(Mapper.Map<VehicleMakeModel>(model));
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await service.DeleteAsync(id);

        }
    }
}