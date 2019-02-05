using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ICars
    {
        Task Delete(Car car);
        Task Clear();
        DemoContext Context { get; set; }
        Task<List<Car>> Get(string carId);
        Task<List<Car>> Get(List<string> carIds, bool includeDrivers = true);
        Task Upsert(Car car, bool driversIncluded = false);
        Task<List<Car>> GetByDriverId(string driverId, bool includeDrivers = false);
        Task<List<Car>> GetByModel(string model);
        Task<List<Car>> GetByModelList(List<string> models);
        Task RemoveRelationships(string driverId, List<string> carIds);
        Task<List<string>> GetCarIds();
    }
}