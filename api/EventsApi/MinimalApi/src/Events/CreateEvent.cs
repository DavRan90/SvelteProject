using Azure;
using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Config;
using MinimalApi.src._internal;
using MinimalApi.src.Categories;

namespace EventsApi.src.Events
{
    public class CreateEvent : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPost("/events", Handle)
            .WithApiDocumentation(
            "CreateEvent",
            "Create event",
            "Create an event.")
            .WithTags("Events");

        public class CreateEventRequest
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? Date { get; set; }
            public int? CategoryId { get; set; }
            public Category? Category { get; set; }
        }

        private static async Task<IResult> Handle(AppDbContext context, CreateEventRequest request)
        {
            if (request.CategoryId is not null)
            {
                var exists = await context.Categories
                    .AnyAsync(c => c.Id == request.CategoryId);

                if (!exists)
                    Results.NotFound();
            }

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                CategoryId = request.CategoryId
            };

            context.Events.Add(newEvent);
            await context.SaveChangesAsync();

            return Results.Ok(newEvent.Id);
        }
    }
}
