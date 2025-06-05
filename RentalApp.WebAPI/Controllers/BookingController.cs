using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Application.Features.ApartmentFeatures.DeleteApartment;
using RentalApp.Application.Features.ApartmentFeatures.GetAllApartment;
using RentalApp.Application.Features.ApartmentFeatures.GetApartmentById;
using RentalApp.Application.Features.ApartmentFeatures.GetAvailableApartment;
using RentalApp.Application.Features.ApartmentFeatures.UpdateApartment;
using RentalApp.Application.Features.BookingFeatures.DeleteBooking;
using RentalApp.Application.Features.BookingFeatures.GetAllBooking;
using RentalApp.Application.Features.BookingFeatures.GetBookingById;
using RentalApp.Application.Features.BookingFeatures.GetUsersBooking;
using RentalApp.Application.Features.Bookings.CreateBooking;

namespace RentalApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBookingResponse>> CreateBooking(
            [FromBody] CreateBookingRequest request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetBookingById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllBookingResponse>>> GetAllBookings(
            CancellationToken cancellationToken)
        {
            var request = new GetAllBookingRequest();
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookingByIdResponse>> GetBookingById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new GetBookingByIdRequest(id);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<List<GetUsersBookingResponse>>> GetUsersBooking(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new GetUsersBookingRequest(id);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteBookingResponse>> DeleteBookingById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new DeleteBookingRequest(id);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateApartmentResponse>> UpdateBooking(
            Guid id, [FromBody] UpdateApartmentRequest updateRequest, 
            CancellationToken cancellationToken)
        {

            var updatedRequest = updateRequest with { Id = id };

            var response = await _mediator.Send(updatedRequest, cancellationToken);
            return Ok(response);
        }
    }
}
