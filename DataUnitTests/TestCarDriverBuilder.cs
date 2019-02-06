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
        private Driver _driver;

        public TestCarDriverBuilder()
        {
            SetUp();
        }

        private void SetUp()
        {
            _carId = "9876543210";
            _driverId = "12365478963";
            _driver = null;
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

    }
}