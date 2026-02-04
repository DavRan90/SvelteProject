using Azure.Core;
using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.src.EventsAlt
{
    public static class EventCreate
    {
        public static async Task<IResult> CreateEvent(AppDbContext context, CreateEventRequest request)
        {
            var handler = new CreateEventHandler(context);
            var id = await handler.Handle(request);
            return Results.Created($"/events/{id}", new { Id = id });
        }
        public static WebApplication MapCreateEventAlt(this WebApplication app)
        {
            app.MapPost("/events", async (CreateEventRequest request, AppDbContext db) =>
            {
                var handler = new CreateEventHandler(db);
                var id = await handler.Handle(request);
                return Results.Created($"/events/{id}", new { Id = id });
            });

            return app;
        }
        public class CreateEventRequest
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? Date { get; set; }
        }

        public class CreateEventHandler
        {
            private readonly AppDbContext _db;
            public CreateEventHandler(AppDbContext db) => _db = db;

            public async Task<int> Handle(CreateEventRequest request)
            {
                var newEvent = new Events.Event
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
}
