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
    public partial class FacturesPage : ContentPage
    {
        FacturesPageVM viewModel;
        public FacturesPage()
        {
            InitializeComponent();
            viewModel = new FacturesPageVM();
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