using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Register
{
    public class DeleteTripsUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext
                .Trips
                .Include(trip => trip.Activities)
                .FirstOrDefault(x => x.Id == id);

            if (trip is null)
                throw new NotFoundException(ResourceErrorMessages.TRAVEL_NOT_EXIST);

            dbContext.Trips.Remove(trip);
            dbContext.SaveChanges();
        }
    }
}
