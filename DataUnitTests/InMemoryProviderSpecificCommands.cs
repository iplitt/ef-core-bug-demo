using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace DataUnitTests
{
    public class InMemoryProviderSpecificCommands : IProviderSpecificCommands
    {
        public void ResetDatabase(DemoContext context)
        {
            context.CarDrivers.RemoveRange(context.CarDrivers);
            context.Drivers.RemoveRange(context.Drivers);
            context.Cars.RemoveRange(context.Cars);
            context.SaveChanges();
        }

        public async Task<List<Driver>> GetDriversForCarIds(DemoContext context, List<string> carIds)
        {
            var drivers = context.Drivers.Where(x => x.CarDrivers.Any(y => carIds.Contains(y.CarId))).ToList();
            return drivers;

        }
    }
}