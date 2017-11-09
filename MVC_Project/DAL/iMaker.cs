using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Project.Models;

namespace MVC_Project.DAL
{
    public interface IMaker
    {
        IEnumerable<Maker> getMakers();
        Maker getMakerById(int makerId);
        void InsertMaker(Maker maker);
        void DeleteMaker(int makerId);
        void UpdateMaker(Maker maker);
        void Save();
    }
}
