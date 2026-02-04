using EventsApi.src.Data;
using EventsApi.src.EventsAlt;


namespace MinimalApi.src.EventsAlt
{
    public static class EventEndpointsAlt
    {
        public static RouteGroupBuilder MapEventEndpointsAlt(this RouteGroupBuilder group)
        {
            group.MapGet("", async (AppDbContext context) =>
                await EventGet.GetEvents(context, 0));

            group.MapGet("/{id:int}", async (AppDbContext context, int id) =>
                await EventGet.GetEvents(context, id));

            group.MapPost("", async (AppDbContext context, EventCreate.CreateEventRequest request) =>
                await EventCreate.CreateEvent(context, request));

            group.MapPut("/{id:int}", async (AppDbContext context, int id, Event updateEvent) =>
                await EventUpdate.UpdateEvent(context, id, updateEvent));

            group.MapDelete("/{id:int}", async (AppDbContext context, int id) =>
                await EventDelete.DeleteEvent(context, id));

            return group;
        }
    }
}
