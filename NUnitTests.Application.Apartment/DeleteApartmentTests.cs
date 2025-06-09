using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.ApartmentFeatures.DeleteApartment;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Application.Appartments
{
    [TestFixture]
    public class DeleteApartmentTests
    {
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private DeleteApartmentHandler _handler;

        [SetUp]
        public void Setup()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<DeleteApartmentMapper>());
            _mapper = config.CreateMapper();

            _handler = new DeleteApartmentHandler(
                _apartmentRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldDeleteApartmentAndReturnResponse()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new DeleteApartmentRequest(apartmentId);

            var existingApartment = new Apartment
            {
                Id = apartmentId,
                Title = "Test Apartment",
                Description = "Test Description",
                Address = "Test Address",
                Rooms = 2,
                PricePerDay = 100,
                DateCreated = DateTime.UtcNow
            };

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(r => r.Get(apartmentId, It.IsAny<CancellationToken>()), Times.Once);
            _apartmentRepositoryMock.Verify(r => r.Delete(existingApartment), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(existingApartment.Id));
            Assert.That(result.Title, Is.EqualTo(existingApartment.Title));
            Assert.That(result.Address, Is.EqualTo(existingApartment.Address));
        }

        [Test]
        public void Handle_ApartmentNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new DeleteApartmentRequest(apartmentId);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Apartment)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"Apartment with Id: {apartmentId} not found!"));
            _apartmentRepositoryMock.Verify(r => r.Delete(It.IsAny<Apartment>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task Handle_ShouldCallSaveAsyncOnce_WhenApartmentExists()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new DeleteApartmentRequest(apartmentId);

            var existingApartment = new Apartment { Id = apartmentId };

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldMapResponseCorrectly_AfterDeletion()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new DeleteApartmentRequest(apartmentId);

            var existingApartment = new Apartment
            {
                Id = apartmentId,
                Title = "Luxury Apartment",
                Description = "Spacious and modern",
                Address = "Central Street 5",
                Rooms = 3,
                PricePerDay = 3000,
                DateCreated = DateTime.UtcNow
            };

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(existingApartment.Id));
            Assert.That(result.Title, Is.EqualTo(existingApartment.Title));
            Assert.That(result.Description, Is.EqualTo(existingApartment.Description));
            Assert.That(result.Address, Is.EqualTo(existingApartment.Address));
            Assert.That(result.Rooms, Is.EqualTo(existingApartment.Rooms));
            Assert.That(result.PricePerDay, Is.EqualTo(existingApartment.PricePerDay));
            Assert.That(result.DateCreated, Is.EqualTo(existingApartment.DateCreated));
        }
    }
}
