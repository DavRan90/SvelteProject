using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Config;
using MinimalApi.src._internal;

public class GetEvent : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events/{id}", Handle)
        .WithApiDocumentation(
        "GetEvent",
        "Get event",
        "Get event by id.")
        .WithTags("Events");

    private static async Task<IResult> Handle(AppDbContext context, int id)
    {
        var ev = await context.Events.FindAsync(id);
        return ev is not null
            ? Results.Ok(ev)
            : Results.NotFound();
    }
}