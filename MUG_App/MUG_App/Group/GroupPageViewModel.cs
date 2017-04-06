using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MUG_App.RestService;

namespace MUG_App.Group
{
    public class GroupPageViewModel : INotifyPropertyChanged
    {
        private string _groupName;
        private string _description;
        private readonly IRestService _restService;

        public GroupPageViewModel(IRestService restService)
        {
            _restService = restService;
        }

        public async Task LoadGroupData()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz";
            var items = await _restService.GetData(restUrl);
            Name = items["name"].ToString();
            Description = items["description"].ToString();
        }

        public string Name
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}