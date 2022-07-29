using ShopSmartDevice.Models;
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
    public partial class PanierPage : ContentPage
    {
        PanierVM viewModel;
        public PanierPage()
        {
            InitializeComponent();
            viewModel = new PanierVM();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.RefreshList();
            BindingContext = null;
            BindingContext = this.viewModel;
        }

 
    }
}