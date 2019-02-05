using Data212;

namespace Data212UnitTests
{
    public class TestDriverBuilder
    {
        private readonly DemoContext _inMemoryDemoContext;
        private string _driverId;
        private string _name;

        public TestDriverBuilder()
        {
            SetUp();
        }

        public TestDriverBuilder(DemoContext inMemoryDemoContext)
        {
            _inMemoryDemoContext = inMemoryDemoContext;
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


            if (_inMemoryDemoContext != null)
                ApplyInMemoryData(driver);

            return driver;
        }

        private void ApplyInMemoryData(Driver driver)
        {
            _inMemoryDemoContext.Drivers.Add(driver);
            _inMemoryDemoContext.SaveChanges();
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