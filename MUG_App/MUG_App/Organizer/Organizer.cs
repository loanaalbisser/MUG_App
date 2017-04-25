namespace MUG_App.Organizer
{
    public class Organizer
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public override string ToString() => Name;
    }
}