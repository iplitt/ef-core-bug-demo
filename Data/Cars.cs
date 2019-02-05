using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Cars : ICars
    {
        private DemoContext _demoContext;

        public DemoContext Context
        {
            get => _demoContext;
            set => _demoContext = value;
        }

        public Cars(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }


        public async Task Delete(Car car)
        {
            _demoContext.Cars.Remove(car);
            await _demoContext.SaveChangesAsync();
        }

        public async Task Clear()
        {
            _demoContext.ProviderSpecificCommands.ResetDatabase(_demoContext);
        }

        public async Task<List<Car>> Get(string carId)
        {
            var cars = _demoContext.Cars.AsNoTracking()
                .Where(x => x.CarId == carId)
                .IncludeChildren()
                .ToList();

            return cars;
        }


        public async Task<List<Car>> Get(List<string> carIds, bool includeDrivers = true)
        {
            var resources = _demoContext.Cars.AsNoTracking()
                .Where(x => carIds.Contains(x.CarId))
                .IncludeChildren(includeDrivers)
                .ToList();

            return resources;
        }

        public async Task<List<Car>> GetByModel(string model)
        {
            var cars = _demoContext.Cars.AsNoTracking()
                .Where(x => x.Model == model)
                .IncludeChildren()
                .ToList();

            return cars;
        }
        public async Task<List<Car>> GetByModelList(List<string> models)
        {
            var cars = _demoContext.Cars.AsNoTracking()
                .Where(x => models.Contains(x.Model))
                .IncludeChildren()
                .ToList();

            return cars;
        }

        public async Task RemoveRelationships(string driverId, List<string> carIds)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Car>> GetByDriverId(string driverId, bool includeDrivers = false)
        {
            var relatedCarIds =
                _demoContext.CarDrivers.AsNoTracking().Where(x => x.DriverId == driverId)
                    .Select(x => x.CarId)
                    .ToList();

            var cars = await Get(relatedCarIds, includeDrivers);
          
            return cars;
        }


        public async Task<List<string>> GetCarIds()
        {
            return _demoContext.Cars.Select(x => x.CarId).OrderBy(y => y).ToList();
        }


        public async Task Upsert(Car car, bool driversIncluded = false)
        {
            var existingCar =
                _demoContext.Cars
                    .Include(x => x.CarDrivers)
                    .SingleOrDefault(x => x.CarId == car.CarId);

            if (existingCar == null)
            {
                _demoContext.Add(car);
            }
            else
            {
                _demoContext.Entry(existingCar).CurrentValues.SetValues(car);

                if (car.CarDrivers != null)
                    InsertUpdateOrDeleteCarDrivers(car.CarDrivers, existingCar.CarDrivers);

            }

            _demoContext.SaveChanges();
        }

        private void InsertUpdateOrDeleteCarDrivers(List<CarDriver> carDrivers,
            List<CarDriver> existingCarDrivers)
        {
            var insertCount = 0;
            var updateCount = 0;
            var deleteCount = 0;

            foreach (var carDriver in carDrivers)
            {
                var existingCarDriver = existingCarDrivers?.SingleOrDefault(x =>
                    x.DriverId == carDriver.DriverId);

                if (existingCarDriver == null)
                {
                    existingCarDrivers.Add(carDriver);
                    insertCount++;
                }
                else
                {
                    _demoContext.Entry(existingCarDriver).CurrentValues.SetValues(carDriver);
                    updateCount++;
                }
            }

            foreach (var existingCarDriver in existingCarDrivers)
            {
                if (!carDrivers.Any(x =>
                    x.DriverId == existingCarDriver.DriverId))
                {
                    _demoContext.Remove(existingCarDriver);
                    deleteCount++;
                }
            }

        }


    }

}