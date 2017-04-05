using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MUG_App.Main
{
    public partial class MainPageMaster : ContentPage
    {
        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMenuItem> MenuItems { get; }
            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
                {
                    new MainPageMenuItem { Id = 0, Title = "Group" },
                    new MainPageMenuItem { Id = 1, Title = "Organizers" },
                    new MainPageMenuItem { Id = 2, Title = "Events" },
                });
            }
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
