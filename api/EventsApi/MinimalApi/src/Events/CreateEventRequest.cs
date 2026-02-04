namespace MinimalApi.src.Events
{
    public class CreateEventRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Date { get; set; }
    }
}
