using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Project.Models;

namespace MVC_Project.DAL
{
    public interface IVehicle<T>
    {
        void Create(T obj);
        T Read(int? id);
        void Update(T obj);
        void Delete(int? id);
     
    }
}

