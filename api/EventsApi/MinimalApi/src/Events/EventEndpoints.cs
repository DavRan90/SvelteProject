using EventsApi.Events;
using EventsApi.src.Data;
using EventsApi.src.Events;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.Events
{
    public static class EventEndpoints
    {
        public static RouteGroupBuilder MapEventEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("", async (AppDbContext context) =>
            await context.Events.ToListAsync());

            group.MapGet("/{id:int}", async (AppDbContext context, int id) =>
            {
                var ev = await context.Events.FindAsync(id);
                return ev is not null ? Results.Ok(ev) : Results.NotFound();
            });

            group.MapPost("", async (CreateEventRequest request, AppDbContext db) =>
            {
                var handler = new CreateEventHandler(db);
                var id = await handler.Handle(request);
                return Results.Created($"/events/{id}", new { Id = id });
            });

            group.MapPut("/{id:int}", async (AppDbContext context, int id, Event updateEvent) =>
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

            group.MapDelete("/{id:int}", async (AppDbContext context, int id) =>
            {
                var ev = await context.Events.FindAsync(id);
                if (ev is null)
                    return Results.NotFound();

                context.Events.Remove(ev);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });           

            return group;
        }
    }
}
