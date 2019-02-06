using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ICars
    {
        Task<List<Car>> Get(string carId);
    }
}