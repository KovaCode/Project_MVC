using Model.Common;
using System;

namespace Model
{
    public class Model : IModel
    {
        public Guid Id { get; set; }
        public Guid VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual Make Make { get; set; }
    }

}
