using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.EventsAlt
{
    public class CategoryGet
    {
        public static async Task<IResult> GetCategories(AppDbContext context, int id)
        {
            if (id == 0)
            {
                var evts = await context.Categories.ToListAsync();
                return Results.Ok(evts);
            }
            else
            {
                var ev = await context.Categories.FindAsync(id);
                return ev is not null
                    ? Results.Ok(ev)
                    : Results.NotFound();
            }
        }
    }
}
