using System.ComponentModel;

namespace MUG_App.Group
{
    public partial class GroupPageViewModel : INotifyPropertyChanged
    {
        public class Group
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public string City { get; set; }

            public string ImageUrl { get; set; }
        }
    }
}