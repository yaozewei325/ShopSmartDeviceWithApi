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
    public partial class PaiementPage : ContentPage
    {
        PaiementPageVM viewModel;
        public PaiementPage()
        {
            InitializeComponent();
            viewModel = new PaiementPageVM();
            BindingContext = this.viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = this.viewModel;
        }

      
    }
}