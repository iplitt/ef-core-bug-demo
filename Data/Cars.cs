using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Cars : ICars
    {
        private readonly DemoContext _demoContext;

        public Cars(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public async Task<List<Car>> Get(string carId)
        {
            var cars = _demoContext.Cars.AsNoTracking()
                .Where(x => x.CarId == carId)
                .Include(x => x.CarDrivers)
                .ToList();

            return cars;
        }
    }
}