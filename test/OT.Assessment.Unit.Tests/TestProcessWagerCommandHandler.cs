using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OT.Assessment.Core.Commands.ProcessWager;
using OT.Assessment.Infrastructure.Context;

namespace OT.Assessment.Unit.Tests
{
    public class TestProcessWagerCommandHandler : TestBase
    {
        private ProcessWagerCommandHandler _sut;
        private ApplicationDbContext _context;
        private string[] themes = { "ancient", "adventure", "wildlife", "jungle", "retro", "family", "crash" };

        [SetUp]
        public void TestProcessWagerCommandHandlerSetUp()
        {
            _context = _serviceProvider.GetService<ApplicationDbContext>()!;
            _sut = new ProcessWagerCommandHandler(_context);
        }

        [Test]
        public async Task Can_Process_Wager()
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(true)
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            await _sut.Handle(wager, CancellationToken.None);

            //Assert
            await Assert.MultipleAsync(async () =>
            {
                Assert.That(await _context.Player.ToListAsync(), Has.Count.GreaterThan(0));
                Assert.That(await _context.Wager.ToListAsync(), Has.Count.GreaterThan(0));
            });
        }

        [Test]
        public async Task Will_Not_Insert_Duplicate_Player()
        {
            //Arrange
            var existingPlayerId = Guid.NewGuid();
            await _context.Player.AddAsync(new Infrastructure.Entities.Player()
            {
                AccountId = existingPlayerId,
                UserName = "Test",
            });
            await _context.SaveChangesAsync();
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(true)
                .RuleFor(o => o.AccountId, existingPlayerId)
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            await _sut.Handle(wager, CancellationToken.None);

            //Assert
            await Assert.MultipleAsync(async () =>
            {
                Assert.That(await _context.Player.ToListAsync(), Has.Count.EqualTo(1));
                Assert.That(await _context.Wager.ToListAsync(), Has.Count.EqualTo(1));
            });
        }

        [TearDown]
        public void TestProcessWagerCommandHandlerTearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
