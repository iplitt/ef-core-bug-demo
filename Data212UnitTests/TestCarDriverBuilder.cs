using Data212;

namespace Data212UnitTests
{
    public class TestCarDriverBuilder
    {
        private string _carId;
        private string _driverId;

        public TestCarDriverBuilder()
        {
            SetUp();
        }

        private void SetUp()
        {
            _carId = "9876543210";
            _driverId = "12365478963";
        }
        public CarDriver Build()
        {
            var carDriver = new CarDriver
            {
                CarId = _carId,
                DriverId = _driverId 
            };

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