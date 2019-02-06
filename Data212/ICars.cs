using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data212
{
    public interface ICars
    {
        Task<List<Car>> Get(string carId);
    }
}