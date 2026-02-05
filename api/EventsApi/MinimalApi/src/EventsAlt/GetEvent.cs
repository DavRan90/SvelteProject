using EventsApi.src.Data;
using MinimalApi.src._internal;

namespace MinimalApi.src.EventsAlt
{
    public class GetEvent : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapGet("/events/{id}", Handle)
            .WithTags("Events");

        private static async Task<IResult> Handle(AppDbContext context, int id)
        {
            var ev = await context.Events.FindAsync(id);
            return ev is not null
                ? Results.Ok(ev)
                : Results.NotFound();
        }
    }
}
