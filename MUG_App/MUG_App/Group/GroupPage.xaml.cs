using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUG_App.Group
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        private readonly GroupPageViewModel _model;

        public GroupPage()
        {
            InitializeComponent();
            _model = new GroupPageViewModel(new RestService.RestService());
            BindingContext = _model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _model.LoadGroupData();
        }
    }
}
