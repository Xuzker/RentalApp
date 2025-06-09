using AutoMapper;
using Moq;
using RentalApp.Application.Features.BookingFeatures.GetUsersBooking;
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
    public class GetUsersBookingTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private IMapper _mapper;
        private GetUsersBookingHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, GetUsersBookingResponse>());
            _mapper = config.CreateMapper();

            _handler = new GetUsersBookingHandler(_userRepositoryMock.Object, _mapper, _bookingRepositoryMock.Object);
        }

        [Test]
        public async Task ShouldReturnUsersBookings()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };
            var bookings = new List<Booking> { new Booking { Id = Guid.NewGuid(), UserId = userId } };

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _bookingRepositoryMock.Setup(r => r.GetUserBookingsAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(bookings);

            var result = await _handler.Handle(new GetUsersBookingRequest(userId), CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
