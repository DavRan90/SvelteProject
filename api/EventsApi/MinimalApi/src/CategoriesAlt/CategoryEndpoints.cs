using EventsApi.src.Data;
using EventsApi.src.Events;
using EventsApi.src.EventsAlt;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Categories;
using MinimalApi.src.EventsAlt;

namespace MinimalApi.src.Events
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("", async (AppDbContext context) =>
                await CategoryGet.GetCategories(context, 0));

            group.MapGet("/{id:int}", async (AppDbContext context, int id) =>
                await CategoryGet.GetCategories(context, id));

            group.MapPost("", async (AppDbContext context, CategoryCreate.CreateCategoryRequest request) =>
                await CategoryCreate.CreateCategory(context, request));

            group.MapPut("/{id:int}", async (AppDbContext context, int id, Category updateCategory) =>
                await CategoryUpdate.UpdateCategory(context, id, updateCategory));

            group.MapDelete("/{id:int}", async (AppDbContext context, int id) =>
                await CategoryDelete.DeleteCategory(context, id));

            return group;
        }
    }
}
