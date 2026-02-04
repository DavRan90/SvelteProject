using EventsApi.src.Data;
using EventsApi.src.EventsAlt;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.EventsAlt
{
    public static class EventUpdate
    {
        public static async Task<IResult> UpdateEvent(AppDbContext context, int id, Event updateEvent)
        {
            var evt = await context.Events.FindAsync(id);
            if (evt is null)
                return Results.NotFound();

            evt.Title = updateEvent.Title;
            evt.Description = updateEvent.Description;
            evt.Date = updateEvent.Date;
            evt.CategoryId = updateEvent.CategoryId;

            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
