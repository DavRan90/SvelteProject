using Azure;
using EventsApi.src.Data;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src._internal;
using MinimalApi.src.Categories;

namespace EventsApi.src.Events
{
    public class CreateCategory : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPost("/categories", Handle)
            .WithTags("Categories");

        public class CreateCategoryRequest
        {
            public string Name { get; set; } = string.Empty;
        }

        private static async Task<int> Handle(AppDbContext context, CreateCategoryRequest request)
        {
            var newCategory = new Category
            {
                Name = request.Name,
            };

            context.Categories.Add(newCategory);
            await context.SaveChangesAsync();

            return newCategory.Id; // returns the new event's Id
        }
    }
}
