using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using NUnit.Framework;

namespace DataUnitTests
{
    [TestFixture]
    public class CarsTests
    {
        private ICars _testObject;
        private DemoContext _inMemoryDemoContext;
        private string _carId;
        private string _driverId1;
        private string _driverId2;

        [SetUp]
        public void SetUp()
        {
            _inMemoryDemoContext =
                new InMemoryDemoContext() { ProviderSpecificCommands = new InMemoryProviderSpecificCommands() };
            _inMemoryDemoContext.Database.EnsureDeleted();

            _carId = "43578";
            _driverId1 = "1";
            _driverId2 = "2";
            var car = new TestCarBuilder().WithCarId(_carId).Build();
            var driver1 = new TestDriverBuilder().WithDriverId(_driverId1).Build();
            var driver2 = new TestDriverBuilder().WithDriverId(_driverId2).Build();
            var carDrivers = new List<CarDriver> { new CarDriver { CarId = _carId, DriverId = _driverId1 } };
            _inMemoryDemoContext.Cars.Add(car);
            _inMemoryDemoContext.Drivers.Add(driver1);
            _inMemoryDemoContext.Drivers.Add(driver2);
            _inMemoryDemoContext.CarDrivers.AddRange(carDrivers);
            _inMemoryDemoContext.SaveChanges();

            _testObject = new Cars(_inMemoryDemoContext);
        }


        [Test]
        public async Task Upsert_AddsNewCarDrivers()
        {
            var existingCar = _inMemoryDemoContext.Cars.Single();

            var newCarDriver = new CarDriver { CarId = _carId, DriverId = _driverId2 };
            existingCar.CarDrivers.Add(newCarDriver);
            await _inMemoryDemoContext.SaveChangesAsync();

            await _testObject.Upsert(existingCar);

            var car = _inMemoryDemoContext.Cars.Single();
            Assert.That(car.CarDrivers.Any(x => x.DriverId == _driverId2), Is.True);
        }


    }
}
