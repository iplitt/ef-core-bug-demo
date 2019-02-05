using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data212;
using NUnit.Framework;

namespace Data212UnitTests
{
    [TestFixture]
    public class CarsTests
    {
        private ICars _testObject;
        private DemoContext _inMemoryDemoContext;

        [SetUp]
        public void SetUp()
        {
            _inMemoryDemoContext =
                new InMemoryDemoContext() { ProviderSpecificCommands = new InMemoryProviderSpecificCommands() };
            _inMemoryDemoContext.Database.EnsureDeleted();

            _testObject = new Cars(_inMemoryDemoContext);
        }


        [Test]
        public async Task Delete_RemoveCarFromTable()
        {
            var car = new Car { CarId = "1234124" };
            _inMemoryDemoContext.Cars.Add(car);
            await _inMemoryDemoContext.SaveChangesAsync();

            await _testObject.Delete(car);

            Assert.That(_inMemoryDemoContext.Cars.Count(), Is.Zero);
        }

        [Test]
        public async Task Upsert_AddsNewCarDrivers()
        {
            const string carId = "43578";
            const string driverId = "342533";
            var existingCarDrivers = new List<CarDriver>();
            var newCarDrivers = new List<CarDriver> { new CarDriver { CarId = carId, DriverId = driverId } };

            var existingCar = new TestCarBuilder().WithCarId(carId).WithCarDrivers(existingCarDrivers).Build();
            var newCar = new TestCarBuilder().WithCarId(carId).WithCarDrivers(newCarDrivers).Build();

            _inMemoryDemoContext.Add(existingCar);
            await _inMemoryDemoContext.SaveChangesAsync();

            await _testObject.Upsert(newCar);

            var carWithId = _inMemoryDemoContext.Cars.Single(x => x.CarId == carId);
            Assert.That(carWithId.CarDrivers.Any(x => x.DriverId == driverId), Is.True);
        }


    }
}
