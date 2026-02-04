using EventsApi.src.Data;
using EventsApi.src.EventsAlt;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Categories;

namespace MinimalApi.src.EventsAlt
{
    public static class CategoryUpdate
    {
        public static async Task<IResult> UpdateCategory(AppDbContext context, int id, Category updateCategory)
        {
            var category = await context.Categories.FindAsync(id);
            if (category is null)
                return Results.NotFound();

            category.Name = updateCategory.Name;

            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
