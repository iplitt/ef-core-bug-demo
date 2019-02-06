using Data212;

namespace Data212UnitTests
{
    public class TestCarBuilder
    {
        private string _model;
        private string _carId;

        public TestCarBuilder()
        {
            SetUp();
        }

        private void SetUp()
        {
            _model = "ABC123";
            _carId = "123";
        }

        public Car Build()
        {
            var car = new Car
            {
                Model = _model,
                CarId = _carId,
            };

            return car;
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

    }
}
