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
            public DateTime? Date { get; set; }
            public int? CategoryId { get; set; }
            public Category? Category { get; set; }
        }

        private static async Task<IResult> Handle(AppDbContext context, CreateEventRequest request)
        {
            var categoryId =
                request.CategoryId is > 0
                    ? request.CategoryId
                    : null;

            if (categoryId is not null)
            {
                var exists = await context.Categories
                    .AnyAsync(c => c.Id == request.CategoryId);

                if (!exists)
                    return Results.NotFound("Category does not exist");
            }
            else
            {
                request.CategoryId = null;
            }

            var newEvent = new Event
                {
                    Title = request.Title,
                    Description = request.Description,
                Date = request.Date is null
                ? null
                : DateTime.SpecifyKind(request.Date.Value, DateTimeKind.Utc),
                CategoryId = request.CategoryId
                };

            context.Events.Add(newEvent);
            await context.SaveChangesAsync();

            return Results.Ok(newEvent.Id);
        }
    }
}
