using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;

public static class GetEvents
{
    public static WebApplication MapGetEvents(this WebApplication app)
    {
        app.MapGet("/events", async (AppDbContext context) =>
            await context.Events.ToListAsync());

        app.MapGet("/events/{id:int}", async (AppDbContext context, int id) =>
        {
            var ev = await context.Events.FindAsync(id);
            return ev is not null ? Results.Ok(ev) : Results.NotFound();
        });

        return app;
    }
}