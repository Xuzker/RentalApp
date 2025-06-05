using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Application.Features.BookingFeatures.GetAllBooking;
using RentalApp.Application.Features.UserFeatures.CreateUser;
using RentalApp.Application.Features.UserFeatures.DeleteUser;
using RentalApp.Application.Features.UserFeatures.GetUserByEmail;
using RentalApp.Application.Features.UserFeatures.GetUserById;

namespace RentalApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(
            [FromBody] CreateUserRequest request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAllUsers(
            CancellationToken cancellationToken)
        {
            var request = new GetAllUserRequest();
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new GetUserByIdRequest(id);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }


        [HttpGet("by-email")]
        public async Task<ActionResult<GetUserByEmailResponse>> GetUserByEmail(
            [FromQuery] string email,
            CancellationToken cancellationToken)
        {
            var request = new GetUserByEmailRequest(email);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUserById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var request = new DeleteUserRequest(id);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
