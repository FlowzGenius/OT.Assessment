using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OT.Assessment.Infrastructure.Context;

namespace OT.Assessment.Unit.Tests
{
    public class TestBase
    {
        public ServiceProvider _serviceProvider;
        
        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            _serviceProvider = services.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            _serviceProvider.Dispose();
        }
    }
}
