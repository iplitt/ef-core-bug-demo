using System;
using System.Linq;
using Data212;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Data212UnitTests
{
    [TestFixture]
    public class Tests
    {
        private DemoContext _inMemoryDemoContext;
        private string _carId;

        [SetUp]
        public void SetUp()
        {
            _carId = "C1";

            _inMemoryDemoContext = new InMemoryDemoContext();
            _inMemoryDemoContext.Database.EnsureDeleted();

            var driverId1 = "D1";
            _inMemoryDemoContext.Cars.Add(new Car{CarId = _carId});
            _inMemoryDemoContext.Drivers.Add(new Driver{DriverId = driverId1});
            _inMemoryDemoContext.CarDrivers.Add(new CarDriver{CarId = _carId, DriverId = driverId1});

            _inMemoryDemoContext.SaveChanges();
        }

        [Test]
        public void IncludeCarDrivers_ExposesOneCarDriver()
        {
            var actual = _inMemoryDemoContext.Cars.Where(x => x.CarId == _carId)
                .Include(x => x.CarDrivers).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(actual));

            Assert.That(actual.Single().CarDrivers.Count, Is.EqualTo(1));
        }

        [Test]
        public void IncludeCarDrivers_ExposesAddedCarDriver()
        {
            var driverId2 = "D2";

            _inMemoryDemoContext.Drivers.Add(new Driver{DriverId = driverId2});
            _inMemoryDemoContext.CarDrivers.Add(new CarDriver{CarId = _carId, DriverId = driverId2});

            _inMemoryDemoContext.SaveChanges();

            var actual = _inMemoryDemoContext.Cars.Where(x => x.CarId == _carId)
                .Include(x => x.CarDrivers).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(actual));

            Assert.That(actual.Single().CarDrivers.Count, Is.EqualTo(2));
        }

    }
}
