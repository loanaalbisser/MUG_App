using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MUG_App.Common;
using MUG_App.RestService;
using Xamarin.Forms;

namespace MUG_App.Event
{
    public class EventPageViewModel : ViewModelBase
    {
        private readonly IRestService _restService;

        public EventPageViewModel(IRestService restService)
        {
            _restService = restService;
            Items = new ObservableCollection<Event>();
            RefreshDataCommand = new Command(async () => await RefreshData(), () => !IsBusy);
        }

        public ObservableCollection<Event> Items { get; }

        public Command RefreshDataCommand { get; }

        protected override void OnIsBusyChanged()
        {
            base.OnIsBusyChanged();
            RefreshDataCommand.ChangeCanExecute();
        }

        private async Task RefreshData()
        {
            IsBusy = true;
            await LoadEvents();
            IsBusy = false;
        }

        private async Task LoadEvents()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz/events";

            var events = await _restService.GetData(restUrl);

            Items.Clear();

            foreach (var element in events)
            {
                Items.Add(new Event {
                            Title = element["name"].ToString(),
                            Description = HtmlFormatter.RemoveHtmlTags(element["description"].ToString()),
                            YesRsvpCount = $"Freie Plätz:{element["yes_rsvp_count"].ToString()}"
                });
            }
        }
    }
}