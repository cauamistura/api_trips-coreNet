using Journey.Communication.Enums;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetByIdTripsUseCase
    {
        public ResponseTripJson Execute(Guid id) 
        {
            var trip = new JourneyDbContext()
                .Trips
                .Include(trip => trip.Activities)
                .FirstOrDefault(x => x.Id == id);            

            if (trip is null) 
                throw new NotFoundException(ResourceErrorMessages.TRAVEL_NOT_EXIST);

            return new ResponseTripJson 
            { 
                Id = trip.Id,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                Activities = trip.Activities.Select(activity => new ResponseActivityJson 
                { 
                    Id = activity.Id,
                    Date = activity.Date,
                    Name = activity.Name,
                    Status = (ActivityStatus) activity.Status
                
                }).ToList() 
            };
        }
    }
}
