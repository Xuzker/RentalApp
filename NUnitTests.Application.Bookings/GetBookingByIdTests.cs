using AutoMapper;
using Moq;
using RentalApp.Application.Features.BookingFeatures.GetBookingById;
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
    public class GetBookingByIdTests
    {
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private IMapper _mapper;
        private GetBookingByIdHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, GetBookingByIdResponse>());
            _mapper = config.CreateMapper();

            _handler = new GetBookingByIdHandler(_bookingRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task ShouldReturnBookingById()
        {
            var booking = new Booking { Id = Guid.NewGuid() };
            _bookingRepositoryMock.Setup(r => r.Get(booking.Id, It.IsAny<CancellationToken>())).ReturnsAsync(booking);

            var result = await _handler.Handle(new GetBookingByIdRequest(booking.Id), CancellationToken.None);

            Assert.That(result.Id, Is.EqualTo(booking.Id));
        }
    }
}
