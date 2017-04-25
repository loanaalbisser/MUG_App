using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUG_App.Event
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventPage : ContentPage
    {
        private readonly EventPageViewModel _model;

        public EventPage()
        {
            InitializeComponent();
            _model = new EventPageViewModel(new RestService.RestService());
            BindingContext = _model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _model.LoadEvents();
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        private async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            
            await Navigation.PushAsync(new EventDetailPage(e));

            //Deselect Event
            ((ListView)sender).SelectedItem = null;
        }
    }
}
