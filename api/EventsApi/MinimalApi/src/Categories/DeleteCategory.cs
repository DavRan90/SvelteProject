using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src._internal;
using System.Reflection.Metadata;

namespace MinimalApi.src.Events
{
    public class DeleteCategory : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapDelete("/categories/{id}", Handle)
            .WithTags("Categories");

        private static async Task<IResult> Handle(AppDbContext context, int id)
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
