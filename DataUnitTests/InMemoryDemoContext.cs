using Data;
using Microsoft.EntityFrameworkCore;

namespace DataUnitTests
{
    public class InMemoryDemoContext : DemoContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Test");
            base.OnConfiguring(optionsBuilder);
        }

        public override void Dispose()
        {
        }
    }
}
