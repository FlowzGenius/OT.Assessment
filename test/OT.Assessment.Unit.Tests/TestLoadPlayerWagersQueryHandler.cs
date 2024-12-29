using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OT.Assessment.Core.Queries.LoadPlayerWagers;
using OT.Assessment.Infrastructure.Context;
using OT.Assessment.Infrastructure.Entities;

namespace OT.Assessment.Unit.Tests
{
    public class TestLoadPlayerWagersQueryHandler : TestBase
    {
        private LoadPlayerWagersQueryHandler _sut;
        private ApplicationDbContext _context;

        [SetUp]
        public void TestLoadPlayerWagersQueryHandlerSetUp()
        {
            _context = _serviceProvider.GetService<ApplicationDbContext>()!;
            _sut = new LoadPlayerWagersQueryHandler(_context, new FakeLogger<LoadPlayerWagersQueryHandler>());
        }

        [Test]
        public async Task Given_query_request_will_paginate()
        {
            //Arrange
            var playerId = Guid.NewGuid();
            var player = new Player() { AccountId = playerId, UserName = "Test User" };
            var wagers = GetWagersForId(playerId);
            var query = new LoadPlayerWagersQuery
            {
                Page = 1,
                PageSize = 1,
                PlayerId = playerId,
            };
            await _context.Player.AddAsync(player);
            await _context.Wager.AddRangeAsync(wagers);
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Total, Is.EqualTo(3));
                Assert.That(result.TotalPages, Is.EqualTo(3));
                Assert.That(result.Page, Is.EqualTo(1));
                Assert.That(result.PageSize, Is.EqualTo(1));
                Assert.That(result.Data.ToList(), Has.Count.EqualTo(1));
            });
        }

        [TearDown]
        public void TestLoadPlayerWagersQueryHandlerTearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        private List<Wager> GetWagersForId(Guid playerId)
             => new List<Wager>
             {
                new Wager()
                {
                    PlayerAccountId = playerId,
                    WagerId = Guid.NewGuid(),
                    Theme = "Theme",
                    Provider = "Provider",
                    Amount = 200,
                    CreationDate = DateTime.UtcNow,
                    GameName = "Test"
                },
                new Wager()
                {
                    PlayerAccountId = playerId,
                    WagerId = Guid.NewGuid(),
                    Theme = "Theme",
                    Provider = "Provider",
                    Amount = 200,
                    CreationDate = DateTime.UtcNow,
                    GameName = "Test"
                },
                new Wager()
                {
                    PlayerAccountId = playerId,
                    WagerId = Guid.NewGuid(),
                    Theme = "Theme",
                    Provider = "Provider",
                    Amount = 200,
                    CreationDate = DateTime.UtcNow,
                    GameName = "Test"
                }
             };
    }
}
