using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUG_App.Organizer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrganizerPage : ContentPage
    {
        private readonly OrganizerPageViewModel _model;

        public OrganizerPage()
        {
            InitializeComponent();
            _model = new OrganizerPageViewModel(new RestService.RestService());
            BindingContext = _model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _model.LoadOrganizers();
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        private async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");
            
            ((ListView)sender).SelectedItem = null;
        }

    }
}
