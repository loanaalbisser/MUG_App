using System.ComponentModel;

namespace MUG_App.Event
{
    public partial class EventPageViewModel : INotifyPropertyChanged
    {
        public class Event
        {
            public string Title { get; set; }
            public string Description { get; set; }

            public override string ToString() => Title;
        }
    }
}