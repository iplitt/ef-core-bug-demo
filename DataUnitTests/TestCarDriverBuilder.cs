using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace DataUnitTests
{
    public class TestCarDriverBuilder
    {
        private string _carId;
        private string _driverId;
        private DateTime? _preReleaseDate;
        private readonly DemoContext _inMemoryDemoContext;
        private Driver _driver;

        public TestCarDriverBuilder()
        {
            SetUp();
        }

        public TestCarDriverBuilder(DemoContext inMemoryDemoContext)
        {
            _inMemoryDemoContext = inMemoryDemoContext;
            SetUp();
        }

        private void SetUp()
        {
            _driverId = "12365478963";
            _carId = "9876543210";
        }
        public CarDriver Build()
        {
            var carDriver = new CarDriver
            {
                CarId = _carId,
                DriverId = _driverId 
            };
            if (_driver != null)
                carDriver.Driver = _driver;

            return carDriver;
        }
        public TestCarDriverBuilder WithDriverId(string driverId)
        {
            _driverId = driverId;
            return this;
        }


        public TestCarDriverBuilder WithCarId(string carId)
        {
            _carId = carId;
            return this;
        }

        public TestCarDriverBuilder WithDriver(Driver driver)
        {
            _driver = driver;
            return this;
        }

        public void AddCarDriversToInMemoryDb(List<CarDriver> carDrivers, string carId)
        {
            for (var i = 0; i < carDrivers.Count; i++)
            {
                var carDriver = carDrivers[i];
                carDriver.CarId = carId;
                if (!_inMemoryDemoContext.Drivers.Any(x => x.DriverId == carDriver.DriverId))
                {
                    var driver = carDriver.Driver ?? new TestDriverBuilder().WithDriverId(carDriver.DriverId ?? (i + 1).ToString()).Build();
                    carDriver.DriverId = driver.DriverId;
                    _inMemoryDemoContext.Drivers.Add(driver);
                }
            }
            _inMemoryDemoContext.CarDrivers.AddRange(carDrivers);

        }

    }
}