using EventsApi.src.Data;
using EventsApi.src.Events;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.Events
{
    public static class UpdateEvent
    {
        public static WebApplication MapUpdateEvent(this WebApplication app)
        {
            app.MapPut("/events/{id:int}", async (AppDbContext context, int id, Event updateEvent) =>
            {
                var ev = await context.Events.FindAsync(id);
                if (ev is null)
                    return Results.NotFound();

                ev.Title = updateEvent.Title;
                ev.Description = updateEvent.Description;
                ev.Date = updateEvent.Date;
                
                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            return app;
        }
    }
}
