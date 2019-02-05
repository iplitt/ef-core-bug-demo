using System;
using System.Collections.Generic;
using System.Security.Claims;
using Data212;

namespace Data212UnitTests
{
    public class TestCarBuilder
    {
        private string _model;
        private string _carId;
        private List<CarDriver> _carDrivers;
        private readonly DemoContext _inMemoryDemoContext;

        public TestCarBuilder()
        {
            SetUp();
        }

        public TestCarBuilder(DemoContext inMemoryDemoContext)
        {
            _inMemoryDemoContext = inMemoryDemoContext;
            SetUp();
        }

        private void SetUp()
        {
            _model = "ABC123";
            _carId = "123";
            _carDrivers = new List<CarDriver> { new TestCarDriverBuilder().Build() };
        }

        public Car Build()
        {

            var car = new Car
            {
                Model = _model,
                CarId = _carId,
            };

            if(_inMemoryDemoContext != null)
                AddInMemoryData(car);
            else
            {
                _carDrivers?.ForEach(x => x.CarId = _carId);
                car.CarDrivers = _carDrivers;
            }
            return car;
        }

        private void AddInMemoryData(Car car)
        {
            _inMemoryDemoContext.Cars.Add(car);

            if (_carDrivers != null)
            {
                new TestCarDriverBuilder(_inMemoryDemoContext).AddCarDriversToInMemoryDb(_carDrivers, _carId);
            }

            _inMemoryDemoContext.SaveChanges();
        }

        public TestCarBuilder WithCarId(string carId)
        {
            _carId = carId;
            return this;
        }

        public TestCarBuilder WithModel(string model)
        {
            _model = model;
            return this;
        }

        public TestCarBuilder WithCarDrivers(List<CarDriver> carDrivers)
        {
            _carDrivers = carDrivers;
            return this;
        }

    }
}
