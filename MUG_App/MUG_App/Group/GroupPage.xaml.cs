using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MUG_App.Group
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        public GroupPage()
        {
            InitializeComponent();
            BindingContext = new GroupPageViewModel();
        }
    }
}
