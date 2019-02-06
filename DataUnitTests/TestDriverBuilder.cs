using Data;

namespace DataUnitTests
{
    public class TestDriverBuilder
    {
        private string _driverId;
        private string _name;

        public TestDriverBuilder()
        {
            SetUp();
        }

        private void SetUp()
        {
            _driverId = "31492192620";
            _name = "Joe";
        }

        public Driver Build()
        {
            var driver = new Driver
            {
                DriverId = _driverId,
                Name = _name
            };

            return driver;
        }

        public TestDriverBuilder WithDriverId(string driverId)
        {
            _driverId = driverId;
            return this;
        }


        public TestDriverBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

    }
}