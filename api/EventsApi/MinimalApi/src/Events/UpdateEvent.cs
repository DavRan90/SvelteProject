using EventsApi.src.Data;
using EventsApi.src.Events;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Config;
using MinimalApi.src._internal;

namespace MinimalApi.src.Events
{
    public class UpdateEvent : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPut("/events/{id}", Handle)
            .WithApiDocumentation(
            "UpdateEvent",
            "Update event",
            "Update event by id.")
            .WithTags("Events");

        private static async Task<IResult> Handle(AppDbContext context, int id, Event updateEvent)
        {
            var evt = await context.Events.FindAsync(id);
            if (evt is null)
                return Results.NotFound();

            evt.Title = updateEvent.Title;
            evt.Description = updateEvent.Description;
            evt.Date = updateEvent.Date;
            evt.CategoryId = updateEvent.CategoryId;

            await context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
