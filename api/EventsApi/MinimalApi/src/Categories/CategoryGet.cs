using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Categories;

namespace MinimalApi.src.EventsAlt
{
    public class CategoryGet
    {
        public static async Task<IResult> GetCategories(AppDbContext context, int id)
        {
            if (id == 0)
            {
                var categories = await context.Categories.OrderBy(c => c.Id).ToListAsync();
                return Results.Ok(categories);
            }
            else
            {
                var category = await context.Categories.FindAsync(id);
                return category is not null
                    ? Results.Ok(category)
                    : Results.NotFound();
            }
        }
    }
}
