using System.Threading.Tasks;
using MUG_App.Common;
using MUG_App.RestService;
using Xamarin.Forms;

namespace MUG_App.Group
{
    public class GroupPageViewModel : ViewModelBase
    {
        private readonly IRestService _restService;
        private string _groupName;
        private string _description;
        private string _imageUrl;

        public GroupPageViewModel(IRestService restService)
        {
            _restService = restService;
            RefreshDataCommand = new Command(async () => await RefreshData(), () => !IsBusy);
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

        public Command RefreshDataCommand { get; }

        protected override void OnIsBusyChanged()
        {
            base.OnIsBusyChanged();
            RefreshDataCommand.ChangeCanExecute();
        }

        private async Task RefreshData()
        {
            IsBusy = true;
            await LoadGroupInfo();
            IsBusy = false;
        }

        private async Task LoadGroupInfo()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz";

            var items = await _restService.GetData(restUrl);

            var group = new Group
            {
                Name = items["name"].ToString(),
                Description = HtmlFormatter.RemoveHtmlTags(items["description"].ToString())
            };

            Name = group.Name;
            Description = group.Description;
            var groupPhoto = items["group_photo"];
            ImageUrl = groupPhoto["photo_link"].ToString();
        }
    }
}