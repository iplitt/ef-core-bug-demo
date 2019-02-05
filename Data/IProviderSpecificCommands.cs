using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IProviderSpecificCommands
    {
        void ResetDatabase(DemoContext context);
        Task<List<Driver>> GetDriversForCarIds(DemoContext context, List<string> carIds);
    }
}