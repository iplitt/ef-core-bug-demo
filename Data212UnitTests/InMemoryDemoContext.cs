using Data212;
using Microsoft.EntityFrameworkCore;

namespace Data212UnitTests
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
