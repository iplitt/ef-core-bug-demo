﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data212;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Data212UnitTests
{
    [TestFixture]
    public class CarsTests
    {
        private ICars _testObject;
        private DemoContext _inMemoryDemoContext;
        private string _carId1;
        private string _carId2;
        private string _driverId1;
        private string _driverId2;
        private string _driverId3;
        private List<Car> _cars;
        private List<CarDriver> _carDrivers;
        private List<Driver> _drivers;

        [SetUp]
        public void SetUp()
        {
            _carId1 = "C1";
            _carId2 = "C2";
            _driverId1 = "D1";
            _driverId2 = "D2";
            _driverId3 = "D3";
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
        public async Task ExposesChildren()
        {
            var newDrivers = new List<Driver>
            {
                new TestDriverBuilder().WithDriverId(_driverId2).Build(),
                new TestDriverBuilder().WithDriverId(_driverId3).Build()
            };
            _drivers.AddRange(newDrivers);

            _cars.Add(new TestCarBuilder().WithCarId(_carId2).Build());

            _carDrivers.AddRange(new List<CarDriver>
            {
                new TestCarDriverBuilder().WithCarId(_carId1).WithDriverId(_driverId2).Build(),
                new TestCarDriverBuilder().WithCarId("C2").WithDriverId(_driverId3).Build()
            });

            _inMemoryDemoContext.SaveChanges();

            var actual = await _testObject.Get(new List<string> {_carId1});
            Console.WriteLine(JsonConvert.SerializeObject(actual));

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
