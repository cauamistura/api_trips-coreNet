using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        public ResponseTripsJson GetAll() 
        {
            var trips = new JourneyDbContext().Trips.ToList();

            return new ResponseTripsJson 
            { 
                Trips = trips.Select(trip => new ResponseShortTripJson 
                { 
                    Id = trip.Id,
                    Name = trip.Name,
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate,                    
                }).ToList(),
            };
        }
    }
}
