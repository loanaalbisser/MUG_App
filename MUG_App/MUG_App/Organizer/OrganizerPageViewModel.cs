using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MUG_App.RestService;
using Xamarin.Forms;

namespace MUG_App.Organizer
{
    public partial class OrganizerPageViewModel : INotifyPropertyChanged
    {
        private readonly IRestService _restService;
        private bool _busy;

        public ObservableCollection<Organizer> Organizers { get; }

        public OrganizerPageViewModel(IRestService restService)
        {
            _restService = restService;
            Organizers = new ObservableCollection<Organizer>();
            RefreshDataCommand = new Command(async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        public async Task LoadOrganizers()
        {
            const string loanaMemberUrl = "https://api.meetup.com/2/member/216711932?key=123e651e3f70711b4b15151d6d671f75&group_urlname=mobile-user-group-zentralschweiz&sign=true";
            const string thomasMemberUrl = "https://api.meetup.com/2/member/184741056?key=123e651e3f70711b4b15151d6d671f75&group_urlname=mobile-user-group-zentralschweiz&sign=true";
            var loanaMember = await _restService.GetData(loanaMemberUrl);
            var thomasMember = await _restService.GetData(thomasMemberUrl);
            var loanaOrganizer = loanaMember["photo"];
            var thomasOrganizer = thomasMember["photo"];
            Organizers.Add(new Organizer { Name = loanaMember["name"].ToString() , City = loanaMember["city"].ToString(), ImageUrl = loanaOrganizer["photo_link"].ToString()});
            Organizers.Add(new Organizer { Name = thomasMember["name"].ToString() , City = thomasMember["city"].ToString(), ImageUrl = thomasOrganizer["photo_link"].ToString()});
        }

        private async Task RefreshData()
        {
            IsBusy = true;
            await LoadOrganizers();
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