using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src._internal;

public class GetCategories : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/categories", Handle)
        .WithTags("Categories");

    private static async Task<IResult> Handle(AppDbContext context)
    {
        var categories = await context.Categories.OrderBy(c => c.Id).ToListAsync();
        return Results.Ok(categories);
    }
}