using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src._internal;
using Npgsql;
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
                return Results.NotFound("Category not found");

            context.Categories.Remove(category);

            try
            {
                await context.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException pgEx &&
                      pgEx.SqlState == PostgresErrorCodes.ForeignKeyViolation)
            {
                return Results.Conflict(new
                {
                    message = "Category cannot be deleted because it is used by one or more events."
                });
            }
        }
    }
}
