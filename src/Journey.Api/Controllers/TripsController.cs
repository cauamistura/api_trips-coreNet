using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {            
            var response = new RegisterTripsUseCase().Execute(request);
            return Created(string.Empty, response);          
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new GetAllTripsUseCase().GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var response = new GetByIdTripsUseCase().Execute(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            new DeleteTripsUseCase().Execute(id);
            return NoContent();
        }
    }
}
