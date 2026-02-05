using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.src.EventsAlt
{
    public static class CategoryDelete
    {
        public static async Task<IResult> DeleteCategory(AppDbContext context, int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category is null)
                return Results.NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
