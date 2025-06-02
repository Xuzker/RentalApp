using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalApp.Application.Features.Apartments.CreateApartment;
using RentalApp.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;

namespace RentalApp.NUnitTests.WebAPI
{
    [TestFixture]
    public class NUnitTestsApartmentController
    {
        private Mock<IMediator> _mediator;
        private ApartmentsController _apartmentsController;


        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _apartmentsController = new ApartmentsController(_mediator.Object);
        }

        [Test]
        public async Task CreateApartment_ValidRequest_ReturnsCreatedAtAction()
        {
            var request = new CreateApartmentRequest(
                Title: "VIP", Description: "relax room", "Chicago", 2001, 7000);
            var response = new CreateApartmentResponse ( 
                Guid.NewGuid(), "VIP", "Chicago", 7000
            );

            _mediator.Setup(mediator => mediator.Send(request, CancellationToken.None))
                .ReturnsAsync(response);

            var result = await _apartmentsController.CreateApartment(request);

            Assert.That(result, Is.InstanceOf<ActionResult<CreateApartmentResponse>>());

            var actionResult = result.Result as CreatedAtActionResult;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(StatusCodes.Status201Created));

            Assert.That(actionResult?.Value, Is.EqualTo(response));
        }

    }
}