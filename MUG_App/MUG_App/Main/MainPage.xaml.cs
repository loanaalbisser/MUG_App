using System;
using MUG_App.Event;
using MUG_App.Organizer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroupPage = MUG_App.Group.GroupPage;

namespace MUG_App.Main
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;
            NavigateTo(item);
            MasterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }


        private void NavigateTo(MainPageMenuItem menuItem)
        {
            if (menuItem == null)
                return;
            if (menuItem.Id == 0)
            {
                Detail = new NavigationPage(new GroupPage())
                {
                    BarBackgroundColor = Color.FromHex("#e51937")
                };
            }
            else if (menuItem.Id == 1)
            {
                Detail = new NavigationPage(new OrganizerPage())
                {
                    BarBackgroundColor = Color.FromHex("#e51937")
                };
            }
            else if (menuItem.Id == 2)
            {
                Detail = new NavigationPage(new EventPage())
                {
                    BarBackgroundColor = Color.FromHex("#e51937")
                };
            }

        }

    }

}
