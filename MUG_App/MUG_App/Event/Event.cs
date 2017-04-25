namespace MUG_App.Event
{
    public class Event
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string YesRsvpCount { get; set; }

        public override string ToString() => $"{Title}: {Description}";
    }
}