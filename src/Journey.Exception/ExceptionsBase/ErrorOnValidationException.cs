using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class ErrorOnValidationException(string message) : JourneyException(message)
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}
