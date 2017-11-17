
namespace Service.DAL
{
    public interface IVehicle<T>
    {
        void Create(T obj);
        T Read(int? id);
        void Update(T obj);
        void Delete(int? id);
     
    }
}

