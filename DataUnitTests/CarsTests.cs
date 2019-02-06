using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataUnitTests
{
    [TestFixture]
    public class CarsTests
    {
        private ICars _testObject;
        private DemoContext _inMemoryDemoContext;
        private string _carId1;
        private string _driverId1;
        private string _driverId2;
        private List<Car> _cars;
        private List<CarDriver> _carDrivers;
        private List<Driver> _drivers;

        [SetUp]
        public void SetUp()
        {
            _carId1 = "C1";
            _driverId1 = "D1";
            _driverId2 = "D2";
            _cars = new List<Car>
            {
                new TestCarBuilder().WithCarId(_carId1).Build()
            };
            _drivers = new List<Driver>
            {
                new TestDriverBuilder().WithDriverId(_driverId1).Build()
            };
            _carDrivers = new List<CarDriver>
            {
                new TestCarDriverBuilder().WithCarId(_carId1).WithDriverId(_driverId1).Build()
            };

            Initialize();

            _testObject = new Cars(_inMemoryDemoContext);
        }

        [Test]
        public async Task ExposesCarDrivers()
        {
            var newDrivers = new List<Driver>
            {
                new TestDriverBuilder().WithDriverId(_driverId2).Build(),
            };
            _drivers.AddRange(newDrivers);

            _carDrivers.AddRange(new List<CarDriver>
            {
                new TestCarDriverBuilder().WithCarId(_carId1).WithDriverId(_driverId2).Build(),
            });

            _inMemoryDemoContext.SaveChanges();

            var actual = await _testObject.Get(new List<string> { _carId1 });
            Console.WriteLine(JsonConvert.SerializeObject(actual));

            Assert.That(actual.Single().CarDrivers.Count, Is.EqualTo(1));
        }

        private void Initialize()
        {
            _inMemoryDemoContext = new InMemoryDemoContext() { ProviderSpecificCommands = new InMemoryProviderSpecificCommands() };
            _inMemoryDemoContext.Database.EnsureDeleted();

            _inMemoryDemoContext.Cars.AddRange(_cars);
            _inMemoryDemoContext.Drivers.AddRange(_drivers);
            _inMemoryDemoContext.CarDrivers.AddRange(_carDrivers);
            _inMemoryDemoContext.SaveChanges();
        }


    }
}
