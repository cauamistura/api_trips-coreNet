using FluentValidation;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripsUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);
            
            var dbContext = new JourneyDbContext();

            var trip = new Trip 
            { 
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbContext.Trips.Add(trip);
            dbContext.SaveChanges();

            return new ResponseShortTripJson 
            { 
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripsValidator().Validate(request);
            if (!validator.IsValid)
            {
                throw new ErrorOnValidationException(validator.Errors[0].ErrorMessage);
            }                
        }
    }

    public class RegisterTripsValidator : AbstractValidator<RequestRegisterTripJson> 
    {
        public RegisterTripsValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_EMPTY);

            RuleFor(x => x.StartDate.Date).GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            RuleFor(x => x).Must(x => x.StartDate.Date <= x.EndDate.Date)
                .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
