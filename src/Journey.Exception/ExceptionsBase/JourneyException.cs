using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public abstract class JourneyException(string message) : SystemException(message)
    {
        public abstract HttpStatusCode StatusCode { get; }        
    }
}
