using System;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataUnitTests
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
            var driverId1 = "D1";

            _inMemoryDemoContext = new InMemoryDemoContext();
            _inMemoryDemoContext.Database.EnsureDeleted();

            _inMemoryDemoContext.Cars.Add(
                new TestCarBuilder().WithCarId(_carId).Build());

            _inMemoryDemoContext.Drivers.Add(
                new TestDriverBuilder().WithDriverId(driverId1).Build());

            _inMemoryDemoContext.CarDrivers.Add(
                new TestCarDriverBuilder().WithCarId(_carId).WithDriverId(driverId1).Build());

            _inMemoryDemoContext.SaveChanges();
        }

        [Test]
        public void Baseline()
        {
            var actual = _inMemoryDemoContext.Cars.Where(x => x.CarId == _carId)
                .Include(x => x.CarDrivers).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(actual));

            Assert.That(actual.Single().CarDrivers.Count, Is.EqualTo(1));
        }

        [Test]
        public void ExposesAddedCarDriver()
        {
            var driverId2 = "D2";

            _inMemoryDemoContext.Drivers.Add(
                new TestDriverBuilder().WithDriverId(driverId2).Build());

            _inMemoryDemoContext.CarDrivers.Add(
                new TestCarDriverBuilder().WithCarId(_carId).WithDriverId(driverId2).Build());

            _inMemoryDemoContext.SaveChanges();

            var actual = _inMemoryDemoContext.Cars.Where(x => x.CarId == _carId)
                .Include(x => x.CarDrivers).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(actual));

            Assert.That(actual.Single().CarDrivers.Count, Is.EqualTo(2));
        }

    }
}
