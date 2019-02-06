using System;
using System.Collections.Generic;
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
        private string _driverId1;
        private string _driverId2;
        private List<Car> _cars;
        private List<CarDriver> _carDrivers;
        private List<Driver> _drivers;

        [SetUp]
        public void SetUp()
        {
            _carId = "C1";
            _driverId1 = "D1";
            _driverId2 = "D2";
            _cars = new List<Car>
            {
                new TestCarBuilder().WithCarId(_carId).Build()
            };
            _drivers = new List<Driver>
            {
                new TestDriverBuilder().WithDriverId(_driverId1).Build()
            };
            _carDrivers = new List<CarDriver>
            {
                new TestCarDriverBuilder().WithCarId(_carId).WithDriverId(_driverId1).Build()
            };

            _inMemoryDemoContext = new InMemoryDemoContext();
            _inMemoryDemoContext.Database.EnsureDeleted();

            _inMemoryDemoContext.Cars.AddRange(_cars);
            _inMemoryDemoContext.Drivers.AddRange(_drivers);
            _inMemoryDemoContext.CarDrivers.AddRange(_carDrivers);
            _inMemoryDemoContext.SaveChanges();
        }

        [Test]
        public void ExposesCarDrivers()
        {
            var newDrivers = new List<Driver>
            {
                new TestDriverBuilder().WithDriverId(_driverId2).Build(),
            };
            _drivers.AddRange(newDrivers);

            _carDrivers.AddRange(new List<CarDriver>
            {
                new TestCarDriverBuilder().WithCarId(_carId).WithDriverId(_driverId2).Build(),
            });

            _inMemoryDemoContext.SaveChanges();

            var actual = _inMemoryDemoContext.Cars.Where(x => x.CarId == _carId)
                .Include(x => x.CarDrivers).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(actual));

            Assert.That(actual.Single().CarDrivers.Count, Is.EqualTo(1));
        }

    }
}
