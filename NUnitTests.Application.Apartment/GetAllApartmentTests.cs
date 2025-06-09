using AutoMapper;
using Moq;
using RentalApp.Application.Features.ApartmentFeatures.GetAllApartment;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;

namespace NUnitTests.Application.Appartments
{
    [TestFixture]
    public class GetAllApartmentTests
    {
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private IMapper _mapper;
        private GetAllApartmentHandler _handler;

        [SetUp]
        public void Setup()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<GetAllApartmentMapper>());
            _mapper = config.CreateMapper();

            _handler = new GetAllApartmentHandler(_apartmentRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnListOfApartments_WhenApartmentsExist()
        {
            // Arrange
            var apartments = new List<Apartment>
            {
                new Apartment { Id = Guid.NewGuid(), Address = "Address 1" },
                new Apartment { Id = Guid.NewGuid(), Address = "Address 2" }
            };

            var expectedResponse = _mapper.Map<List<GetAllApartmentResponse>>(apartments);

            _apartmentRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(apartments);

            var request = new GetAllApartmentRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(repo => repo.GetAll(It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(expectedResponse[0].Id));
            Assert.That(result[0].Address, Is.EqualTo(expectedResponse[0].Address));
            Assert.That(result[1].Id, Is.EqualTo(expectedResponse[1].Id));
            Assert.That(result[1].Address, Is.EqualTo(expectedResponse[1].Address));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoApartmentsExist()
        {
            // Arrange
            var emptyApartmentsList = new List<Apartment>();

            _apartmentRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(emptyApartmentsList);

            var request = new GetAllApartmentRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(repo => repo.GetAll(It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }
    }
}
