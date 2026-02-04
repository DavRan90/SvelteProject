using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.EventsAlt
{
    public class EventGet
    {
        public static async Task<IResult> GetEvents(AppDbContext context, int id)
        {
            if (id == 0)
            {
                var evts = await context.Events.ToListAsync();
                return Results.Ok(evts);
            }
            else
            {
                var ev = await context.Events.FindAsync(id);
                return ev is not null
                    ? Results.Ok(ev)
                    : Results.NotFound();
            }
        }
    }
}
