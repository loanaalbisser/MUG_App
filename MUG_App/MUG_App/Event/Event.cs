namespace MUG_App.Event
{
    public class Event
    {
        public Event()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public override string ToString() => $"{Title}: {Description}";
    }
}