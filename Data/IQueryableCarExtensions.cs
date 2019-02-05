using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class IQueryableCarExtensions
    {
     
        public static IQueryable<Car> IncludeChildren(this IQueryable<Car> carQuery, bool includeDrivers = true)
        {
            if (includeDrivers)
                carQuery = carQuery.Include(x => x.CarDrivers);

            return carQuery;
        }
    }
}