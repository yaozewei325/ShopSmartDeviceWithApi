using ShopSmartDevice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopSmartDevice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionItemPage : ContentPage
    {
        public GestionItemPage()
        {
            InitializeComponent();
            this.BindingContext = new GestionItemPageVM();

        }
    }
}