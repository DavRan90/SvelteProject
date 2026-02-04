using MinimalApi.src.Categories;

namespace EventsApi.src.EventsAlt
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
        public bool Editing { get; set; }
        public Category? Category { get; set; }
    }
}
