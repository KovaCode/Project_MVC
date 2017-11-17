using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;

namespace Service.DAL
{
    public interface IMaker
    {
        IEnumerable<VechicleMake> getMakers();
        VechicleMake getMakerById(int makerId);
        void InsertMaker(VechicleMake maker);
        void DeleteMaker(int makerId);
        void UpdateMaker(VechicleMake maker);
        void Save();
    }
}
