using System.ComponentModel;

namespace MUG_App.Organizer
{
    public partial class OrganizerPageViewModel : INotifyPropertyChanged
    {
        public class Organizer
        {
            public string Name { get; set; }
            public string City { get; set; }

            public override string ToString() => Name;
        }
    }
}