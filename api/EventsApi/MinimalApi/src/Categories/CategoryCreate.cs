using Azure.Core;
using EventsApi.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Categories;

namespace EventsApi.src.EventsAlt
{
    public static class CategoryCreate
    {
        public static async Task<IResult> CreateCategory(AppDbContext context, CreateCategoryRequest request)
        {
            var handler = new CreateCategoryHandler(context);
            var id = await handler.Handle(request);
            return Results.Created($"/events/{id}", new { Id = id });
        }
        public class CreateCategoryRequest
        {
            public string Name { get; set; } = string.Empty;
        }

        public class CreateCategoryHandler
        {
            private readonly AppDbContext _context;
            public CreateCategoryHandler(AppDbContext context) => _context = context;

            public async Task<int> Handle(CreateCategoryRequest request)
            {
                var newCategory = new Category
                {
                    Name = request.Name,
                };

                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();

                return newCategory.Id; // returns the new event's Id
            }
        }
    }
}
