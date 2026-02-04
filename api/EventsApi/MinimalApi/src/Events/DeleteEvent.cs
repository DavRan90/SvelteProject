using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.Events
{
    public static class DeleteEvent
    {
        public static WebApplication MapDeleteEvent(this WebApplication app)
        {
            app.MapDelete("/events/{id:int}", async (AppDbContext context, int id) =>
            {
                var ev = await context.Events.FindAsync(id);
                if (ev is null)
                    return Results.NotFound();

                context.Events.Remove(ev);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });

            return app;
        }
    }
}
