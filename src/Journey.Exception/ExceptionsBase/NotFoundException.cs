using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException(string message) : JourneyException(message)
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
