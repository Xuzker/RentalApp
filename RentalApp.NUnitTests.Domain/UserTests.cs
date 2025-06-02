using RentalApp.Domain.Entities;

namespace RentalApp.NUnitTests.Domain
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void CreateUser()
        {
            // AAA - ������� 

            // Arrange - ����������
            var email = "valera228@yandex.ru";
            var name = "Valera228";

            // Act - ��������
            var user = new User 
            { 
                Email = email, 
                Name = name 
            };

            // Assert - ��������
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.Name, Is.EqualTo(name));
        }
    }
}