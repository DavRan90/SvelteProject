using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Config;
using MinimalApi.src._internal;
using System.Reflection.Metadata;

namespace MinimalApi.src.Events
{
    public class DeleteEvent : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapDelete("/events/{id}", Handle)
            .WithApiDocumentation(
            "DeleteEvent",
            "Delete event",
            "Delete a event by ID.")
            .WithTags("Events");

        private static async Task<IResult> Handle(AppDbContext context, int id)
        {
            var evt = await context.Events.FindAsync(id);
            if (evt is null)
                return Results.NotFound();

            context.Events.Remove(evt);
            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
