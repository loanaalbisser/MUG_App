using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MUG_App.Common;
using MUG_App.RestService;

namespace MUG_App.Group
{
    public partial class GroupPageViewModel : INotifyPropertyChanged
    {
        private string _groupName;
        private string _description;
        private string _imageUrl;
        private readonly IRestService _restService;

        public GroupPageViewModel(IRestService restService)
        {
            _restService = restService;
        }

        public async Task LoadGroupData()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz";
            var items = await _restService.GetData(restUrl);
            
            var group = new Group()
            {
                Name = items["name"].ToString(),
                Description = HtmlFormatter.RemoveHtmlTags(items["description"].ToString())
            };
            Name = group.Name;
            Description = group.Description;
            var groupPhoto = items["group_photo"];
            ImageUrl = groupPhoto["photo_link"].ToString();
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

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}