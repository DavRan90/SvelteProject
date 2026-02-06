using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Config;
using MinimalApi.src._internal;

public class GetEvents : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events", Handle)
        .WithApiDocumentation(
        "GetEvents",
        "Get events",
        "Get events.")
        .WithTags("Events");

    private static async Task<IResult> Handle(AppDbContext context)
    {
        var evts = await context.Events.OrderBy(e => e.Id).ToListAsync();
        return Results.Ok(evts);
    }
}