using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
                SettErrorByClass(context, (int)((JourneyException)context.Exception).StatusCode);
            else
                SettErrorByClass(context, StatusCodes.Status500InternalServerError, ResourceErrorMessages.UNKNOWN_ERROR);            
        }

        private void SettErrorByClass(ExceptionContext context, int statusCode, string messageError = "")
        {
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(string.IsNullOrEmpty(messageError) ? context.Exception.Message : messageError);
        }
    }
}