using Data;

namespace DataUnitTests
{
    public class TestCarBuilder
    {
        private string _carId;

        public TestCarBuilder()
        {
            SetUp();
        }

        private void SetUp()
        {
            _carId = "123";
        }

        public Car Build()
        {
            var car = new Car
            {
                CarId = _carId,
            };

            return car;
        }

        public TestCarBuilder WithCarId(string carId)
        {
            _carId = carId;
            return this;
        }

    }
}
