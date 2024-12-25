using Bogus;
using NUnit.Framework;
using OT.Assessment.Core.Commands.ProcessWager;
using FluentValidation.TestHelper;

namespace OT.Assessment.Unit.Tests
{
    public class TestProcessWagerCommandHandlerValidator
    {
        private ProcessWagerCommandValidator _sut;
        private string[] themes = { "ancient", "adventure", "wildlife", "jungle", "retro", "family", "crash" };

        [SetUp]
        public void SetUp()
        {
            _sut = new ProcessWagerCommandValidator();
        }

        [Test]
        public async Task Will_Throw_Error_If_WagerId_Is_Not_Set()
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(false)
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.WagerId);
        }

        [Test]
        public async Task Will_Throw_Error_If_AccountId_Is_Not_Set()
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(false)
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.AccountId);
        }

        [Test]
        public async Task Will_Throw_Error_If_Amount_Is_Not_Set()
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(false)
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public async Task Will_Throw_Error_If_Provider_Is_Null_Or_Empty(string? provider)
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(true)
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => provider)
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Provider);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public async Task Will_Throw_Error_If_Theme_Is_Null_Or_Empty(string? theme)
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(true)
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => theme)
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Theme);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public async Task Will_Throw_Error_If_GameName_Is_Null_Or_Empty(string? gameName)
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(true)
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => gameName)
                .RuleFor(o => o.Username, f => f.Person.FirstName)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.GameName);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public async Task Will_Throw_Error_If_UserName_Is_Null_Or_Empty(string? username)
        {
            //Arrange
            var wager = new Faker<ProcessWagerCommand>()
                .StrictMode(true)
                .RuleFor(o => o.AccountId, f => f.Random.Guid())
                .RuleFor(o => o.WagerId, f => f.Random.Guid())
                .RuleFor(o => o.Theme, f => f.PickRandom(themes))
                .RuleFor(o => o.Provider, f => f.Company.CompanyName())
                .RuleFor(o => o.GameName, f => f.Company.CompanyName())
                .RuleFor(o => o.Username, f => username)
                .RuleFor(o => o.CreationDate, DateTime.UtcNow)
                .RuleFor(o => o.Amount, f => f.Random.Decimal())
                .Generate();

            //Act
            var result = await _sut.TestValidateAsync(wager);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }
    }
}
