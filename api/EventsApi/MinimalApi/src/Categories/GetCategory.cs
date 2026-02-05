using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src._internal;

public class GetCategory : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/categories/{id}", Handle)
        .WithTags("Categories");

    private static async Task<IResult> Handle(AppDbContext context, int id)
    {
        var category = await context.Categories.FindAsync(id);
        return category is not null
            ? Results.Ok(category)
            : Results.NotFound();
    }
}