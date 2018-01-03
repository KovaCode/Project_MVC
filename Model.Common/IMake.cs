using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public interface IMake : IEntity
    {
        new Guid Id { get; set; }
        new String Name { get; set; }
        new String Abrv { get; set; }
    }
}
