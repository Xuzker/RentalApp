using AutoMapper;
using Moq;
using RentalApp.Application.Features.BookingFeatures.GetAllBooking;
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
    public class GetAllBookingTests
    {
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private IMapper _mapper;
        private GetAllBookingHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, GetAllBookingResponse>());
            _mapper = config.CreateMapper();

            _handler = new GetAllBookingHandler(_bookingRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task ShouldReturnAllBookings()
        {
            var bookings = new List<Booking> { new Booking { Id = Guid.NewGuid() } };
            _bookingRepositoryMock.Setup(r => r.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(bookings);

            var result = await _handler.Handle(new GetAllBookingRequest(), CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(1));
        }
    }

}
