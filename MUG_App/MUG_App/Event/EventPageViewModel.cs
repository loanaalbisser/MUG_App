using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MUG_App.Common;
using MUG_App.RestService;
using Xamarin.Forms;

namespace MUG_App.Event
{
    public partial class EventPageViewModel : INotifyPropertyChanged
    {
        private readonly IRestService _restService;
        private bool _busy;
        public ObservableCollection<Event> Items { get; }


        public EventPageViewModel(IRestService restService)
        {
            _restService = restService;
            Items = new ObservableCollection<Event>();
            
            RefreshDataCommand = new Command(async () => await RefreshData());
        }

        public async Task LoadEvents()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz/events";
            var events = await _restService.GetData(restUrl);
            foreach (var element in events)
            {
                Items.Add(new Event {Title = element["name"].ToString(), Description = HtmlFormatter.RemoveHtmlTags(element["description"].ToString())});
            }
        }

        public ICommand RefreshDataCommand { get; }

        private async Task RefreshData()
        {
            IsBusy = true;
            await LoadEvents();

            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return _busy; }
            set
            {
                _busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
    }
}