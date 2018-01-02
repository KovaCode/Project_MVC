using Model.Common;
using System;

namespace Model
{
    public class BaseEntity : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Abrv { get; set; }
        public virtual string Name { get; set; }
    }

}
