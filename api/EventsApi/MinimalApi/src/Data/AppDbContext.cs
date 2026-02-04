using EventsApi.src.Events;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Categories;

namespace EventsApi.src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories{ get; set; }
    }
}
