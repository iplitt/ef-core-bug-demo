using Data;

namespace DataUnitTests
{
    public class TestDriverBuilder
    {
        private string _driverId;

        public TestDriverBuilder()
        {
            SetUp();
        }

        private void SetUp()
        {
            _driverId = "31492192620";
        }

        public Driver Build()
        {
            var driver = new Driver
            {
                DriverId = _driverId,
            };

            return driver;
        }

        public TestDriverBuilder WithDriverId(string driverId)
        {
            _driverId = driverId;
            return this;
        }

    }
}