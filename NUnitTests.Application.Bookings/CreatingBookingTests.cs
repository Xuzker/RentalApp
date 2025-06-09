using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.Bookings.CreateBooking;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Application.Bookings
{
    [TestFixture]
    public class CreateBookingTests
    {
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private CreateBookingHandler _handler;

        [SetUp]
        public void Setup()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateBookingRequest, Booking>();
                cfg.CreateMap<Booking, CreateBookingResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateBookingHandler(_unitOfWorkMock.Object, _apartmentRepositoryMock.Object, _bookingRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task ShouldCreateBookingSuccessfully()
        {
            var apartment = new Apartment { Id = Guid.NewGuid(), PricePerDay = 100, IsAvailable = true };
            var request = new CreateBookingRequest(apartment.Id, Guid.NewGuid(), DateTime.Today, DateTime.Today.AddDays(2));

            _apartmentRepositoryMock.Setup(r => r.Get(request.ApartmentId, It.IsAny<CancellationToken>())).ReturnsAsync(apartment);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.That(result.TotalPrice, Is.EqualTo(200));
            Assert.That(result.Status, Is.EqualTo("Pending"));
        }

        [Test]
        public void ShouldThrow_WhenApartmentNotAvailable()
        {
            var apartment = new Apartment { Id = Guid.NewGuid(), IsAvailable = false };
            var request = new CreateBookingRequest(apartment.Id, Guid.NewGuid(), DateTime.Today, DateTime.Today.AddDays(1));

            _apartmentRepositoryMock.Setup(r => r.Get(request.ApartmentId, It.IsAny<CancellationToken>())).ReturnsAsync(apartment);

            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
