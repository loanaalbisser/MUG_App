using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUG_App.Organizer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrganizerPage : ContentPage
    {
        private readonly OrganizerPageViewModel _viewModel;

        public OrganizerPage()
        {
            InitializeComponent();
            _viewModel = new OrganizerPageViewModel(new RestService.RestService());
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.RefreshDataCommand.Execute(null);
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