using EventsApi.Events;
using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Events;

namespace EventsApi.src.Events
{
    public static class CreateEvent
    {
        public static WebApplication MapCreateEvent(this WebApplication app)
        {
            app.MapPost("/events", async (CreateEventRequest request, AppDbContext db) =>
            {
                var handler = new CreateEventHandler(db);
                var id = await handler.Handle(request);
                return Results.Created($"/events/{id}", new { Id = id });
            });

            return app;
        }
    }
}
