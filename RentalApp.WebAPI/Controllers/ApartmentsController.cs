using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Application.Features.ApartmentFeatures.DeleteApartment;
using RentalApp.Application.Features.ApartmentFeatures.GetAllApartment;
using RentalApp.Application.Features.ApartmentFeatures.GetApartmentById;
using RentalApp.Application.Features.ApartmentFeatures.GetAvailableApartment;
using RentalApp.Application.Features.ApartmentFeatures.UpdateApartment;
using RentalApp.Application.Features.Apartments.CreateApartment;

namespace RentalApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateApartmentResponse>> CreateApartment(
            [FromBody] CreateApartmentRequest request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetApartmentById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllApartmentResponse>>> GetAllApartments(
            CancellationToken cancellationToken)
        {
            var request = new GetAllApartmentRequest();
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetApartmentByIdResponse>> GetApartmentById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new GetApartmentByIdRequest(id);
            var response = await _mediator.Send(request, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("available")]
        public async Task<ActionResult<List<GetAvailableApartmentResponse>>> GetAvailableApartments(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken cancellationToken)
        {
            var request = new GetAvailableApartmentRequest(from, to);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteApartmentResponse>> DeleteApartmentById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new DeleteApartmentRequest(id);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateApartmentResponse>> UpdateApartment(
            Guid id, [FromBody] UpdateApartmentRequest updateRequest)
        {
            var updatedRequest = updateRequest with { Id = id };

            var response = await _mediator.Send(updatedRequest);
            return Ok(response);
        }
    }
}
