using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.EventsAlt
{
    public static class EventDelete
    {

        public static async Task<IResult> DeleteEvent(AppDbContext context, int id)
        {
            var evt = await context.Events.FindAsync(id);
            if (evt is null)
                return Results.NotFound();

            context.Events.Remove(evt);
            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
