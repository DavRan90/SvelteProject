using EventsApi.src.Data;
using EventsApi.src.Events;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Events;

namespace EventsApi.Events
{
    public class CreateEventHandler
    {
        private readonly AppDbContext _db;
        public CreateEventHandler(AppDbContext db) => _db = db;

        public async Task<int> Handle(CreateEventRequest request)
        {
            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date
            };

            _db.Events.Add(newEvent);
            await _db.SaveChangesAsync();

            return newEvent.Id; // returns the new event's Id
        }
    }
}
