using Azure.Core;
using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Categories;

namespace EventsApi.src.EventsAlt
{
    public static class EventCreate
    {
        public static async Task<IResult> CreateEvent(AppDbContext context, CreateEventRequest request)
        {
            var handler = new CreateEventHandler(context);
            var id = await handler.Handle(request);
            return Results.Created($"/events/{id}", new { Id = id });
        }
        public class CreateEventRequest
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? Date { get; set; }
            public int? CategoryId { get; set; }
            public Category? Category { get; set; }
        }

        public class CreateEventHandler
        {
            private readonly AppDbContext _context;
            public CreateEventHandler(AppDbContext context) => _context = context;

            public async Task<int> Handle(CreateEventRequest request)
            {
                if (request.CategoryId is not null)
                {
                    var exists = await _context.Categories
                        .AnyAsync(c => c.Id == request.CategoryId);

                    if (!exists)
                        throw new InvalidOperationException("Category does not exist");
                }

                var newEvent = new Events.Event
                {
                    Title = request.Title,
                    Description = request.Description,
                    Date = request.Date,
                    CategoryId = request.CategoryId
                };

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return newEvent.Id;
            }
        }
    }
}
