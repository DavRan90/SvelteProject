using EventsApi.src.Data;
using EventsApi.src.Events;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src._internal;
using MinimalApi.src.CategoriesAlt;

namespace MinimalApi.src.Events
{
    public class UpdateCategory : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPut("/categories/{id}", Handle)
            .WithTags("Categories");

        private static async Task<IResult> Handle(AppDbContext context, int id, Category updateCategory)
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
