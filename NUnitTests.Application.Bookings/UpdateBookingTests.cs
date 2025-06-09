using AutoMapper;
using Moq;
using RentalApp.Application.Features.BookingFeatures.UpdateBooking;
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
    public class UpdateBookingTests
    {
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private UpdateBookingHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<UpdateBookingRequest, Booking>();
                cfg.CreateMap<Booking, UpdateBookingResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateBookingHandler(_bookingRepositoryMock.Object, _mapper, _unitOfWorkMock.Object);
        }

        [Test]
        public async Task ShouldUpdateBookingSuccessfully()
        {
            var bookingId = Guid.NewGuid();
            var booking = new Booking
            {
                Id = bookingId,
                ApartmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                TotalPrice = 100
            };

            var request = new UpdateBookingRequest(bookingId, booking.ApartmentId, booking.UserId, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3), 300);

            _bookingRepositoryMock.Setup(r => r.Get(bookingId, It.IsAny<CancellationToken>())).ReturnsAsync(booking);
            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.That(result.TotalPrice, Is.EqualTo(300));
            Assert.That(result.EndDate, Is.EqualTo(DateTime.Today.AddDays(3)));
        }
    }
}
