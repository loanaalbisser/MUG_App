using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MUG_App.Common;
using MUG_App.RestService;
using Xamarin.Forms;

namespace MUG_App.Organizer
{
    public class OrganizerPageViewModel : ViewModelBase
    {
        private readonly IRestService _restService;

        public OrganizerPageViewModel(IRestService restService)
        {
            _restService = restService;
            Organizers = new ObservableCollection<Organizer>();
            RefreshDataCommand = new Command(async () => await RefreshData(), () => !IsBusy);
        }

        public ObservableCollection<Organizer> Organizers { get; }

        public Command RefreshDataCommand { get; }

        protected override void OnIsBusyChanged()
        {
            base.OnIsBusyChanged();

            RefreshDataCommand.ChangeCanExecute();
        }

        private async Task RefreshData()
        {
            IsBusy = true;
            await LoadOrganizers();
            IsBusy = false;
        }

        private async Task LoadOrganizers()
        {
            const string loanaMemberUrl = "https://api.meetup.com/2/member/216711932?key=123e651e3f70711b4b15151d6d671f75&group_urlname=mobile-user-group-zentralschweiz&sign=true";
            const string thomasMemberUrl = "https://api.meetup.com/2/member/184741056?key=123e651e3f70711b4b15151d6d671f75&group_urlname=mobile-user-group-zentralschweiz&sign=true";

            var loanaMember = await _restService.GetData(loanaMemberUrl);
            var thomasMember = await _restService.GetData(thomasMemberUrl);

            var loanaOrganizer = loanaMember["photo"];
            var thomasOrganizer = thomasMember["photo"];

            Organizers.Clear();
            Organizers.Add(new Organizer {
                Name = $"Name: {loanaMember["name"].ToString()}",
                City = $"Wohnort: {loanaMember["city"].ToString()}",
                ImageUrl = loanaOrganizer["photo_link"].ToString() });
            Organizers.Add(new Organizer
            {
                Name = $"Name: {thomasMember["name"].ToString()}",
                City = $"Wohnort: {thomasMember["city"].ToString()}",
                ImageUrl = thomasOrganizer["photo_link"].ToString()
            });
        }
    }
}