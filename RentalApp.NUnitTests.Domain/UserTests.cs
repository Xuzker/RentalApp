using RentalApp.Domain.Entities;

namespace RentalApp.NUnitTests.Domain
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void CreateUser()
        {
            // AAA - паттерн 

            // Arrange - подготовка
            var email = "valera228@yandex.ru";
            var name = "Valera228";

            // Act - действие
            var user = new User 
            { 
                Email = email, 
                Name = name 
            };

            // Assert - проверка
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.Name, Is.EqualTo(name));
        }
    }
}