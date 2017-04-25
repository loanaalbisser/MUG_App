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
        
    }
}