using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using RentalApp.Infrastructure.Context;
using RentalApp.Infrastructure.Repositories;

namespace RentalApp.NUnitTests.Infrasctructre
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private DataContext _context;
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _userRepository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetByEmailAndName()
        {
            // AAA - паттерн 

            // Arrange - подготовка
            var email = "valera228@yandex.ru";
            var name = "Valera228";

            var user = new User
            {
                Email = email,
                Name = name
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act - действие
            var result = await _userRepository.GetByEmailAsync(email, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Email, Is.EqualTo(email));
            Assert.That(result?.Name, Is.EqualTo(name));
        }
    }
}