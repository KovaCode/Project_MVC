using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public interface IEntity
    {
        Guid Id { get; set; }
        String Name { get; set; }
        String Abrv { get; set; }
    }
}
