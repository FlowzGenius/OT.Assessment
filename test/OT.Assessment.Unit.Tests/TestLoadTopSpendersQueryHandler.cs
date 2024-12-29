using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OT.Assessment.Core.Queries.LoadTopSpenders;
using OT.Assessment.Infrastructure.Context;
using OT.Assessment.Infrastructure.Entities;

namespace OT.Assessment.Unit.Tests
{
    public class TestLoadTopSpendersQueryHandler : TestBase
    {
        private LoadTopSpendersQueryHandler _sut;
        private ApplicationDbContext _context;

        [SetUp]
        public void TestLoadTopSpendersQueryHandlerSetUp()
        {
            _context = _serviceProvider.GetService<ApplicationDbContext>()!;
            _sut = new LoadTopSpendersQueryHandler(_context, new FakeLogger<LoadTopSpendersQueryHandler>());
        }

        [TestCase(2)]
        [TestCase(5)]
        [TestCase(4)]
        public async Task Will_Return_Correct_Count(int count)
        {
            //Arrange
            await GenerateTestData();
            var query = new LoadTopSpendersQuery { Count = count };

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result, Has.Count.EqualTo(count));
        }

        [Test]
        public async Task Will_Return_Top_Spender()
        {
            //Arrange
            await GenerateTestData();
            var query = new LoadTopSpendersQuery { Count = 5 };

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.First().Username, Is.EqualTo("Player6"));
                Assert.That(result.First().TotalAmountSpend, Is.EqualTo(841.50));
            });
        }

        [TearDown]
        public void TestLoadTopSpendersQueryHandlerTearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        public async Task GenerateTestData()
        {
            var players = new List<Player>
            {
                new Player {AccountId = Guid.NewGuid(), UserName = "Player1"},
                new Player {AccountId = Guid.NewGuid(), UserName = "Player2"},
                new Player {AccountId = Guid.NewGuid(), UserName = "Player3"},
                new Player {AccountId = Guid.NewGuid(), UserName = "Player4"},
                new Player {AccountId = Guid.NewGuid(), UserName = "Player5"},
                new Player {AccountId = Guid.NewGuid(), UserName = "Player6"},
            };
            List<Wager> wagers = [];

            var amount = (decimal)30.50;
            foreach (var player in players)
            { 
                wagers.AddRange(GenerateWagersForPlayerId(player.AccountId, amount));
                amount += 50;
            }

            await _context.Player.AddRangeAsync(players);
            await _context.Wager.AddRangeAsync(wagers);
            await _context.SaveChangesAsync();
        }

        public List<Wager> GenerateWagersForPlayerId(Guid playerId, decimal amount)
            => new List<Wager>
             {
                new Wager()
                {
                    PlayerAccountId = playerId,
                    WagerId = Guid.NewGuid(),
                    Theme = "Theme",
                    Provider = "Provider",
                    Amount = amount,
                    CreationDate = DateTime.UtcNow,
                    GameName = "Test"
                },
                new Wager()
                {
                    PlayerAccountId = playerId,
                    WagerId = Guid.NewGuid(),
                    Theme = "Theme",
                    Provider = "Provider",
                    Amount = amount,
                    CreationDate = DateTime.UtcNow,
                    GameName = "Test"
                },
                new Wager()
                {
                    PlayerAccountId = playerId,
                    WagerId = Guid.NewGuid(),
                    Theme = "Theme",
                    Provider = "Provider",
                    Amount = amount,
                    CreationDate = DateTime.UtcNow,
                    GameName = "Test"
                }
             };
    }
}
