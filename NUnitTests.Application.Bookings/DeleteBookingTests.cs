using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.BookingFeatures.DeleteBooking;
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
    public class DeleteBookingTests
    {
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private DeleteBookingHandle _handler;

        [SetUp]
        public void Setup()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, DeleteBookingResponse>());
            _mapper = config.CreateMapper();

            _handler = new DeleteBookingHandle(_bookingRepositoryMock.Object, _mapper, _unitOfWorkMock.Object);
        }

        [Test]
        public async Task ShouldDeleteBookingSuccessfully()
        {
            var booking = new Booking { Id = Guid.NewGuid() };
            var request = new DeleteBookingRequest(booking.Id);

            _bookingRepositoryMock.Setup(r => r.Get(request.Id, It.IsAny<CancellationToken>())).ReturnsAsync(booking);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.That(result.Id, Is.EqualTo(booking.Id));
        }

        [Test]
        public void ShouldThrow_WhenBookingNotFound()
        {
            var request = new DeleteBookingRequest(Guid.NewGuid());

            _bookingRepositoryMock.Setup(r => r.Get(request.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Booking)null);

            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
