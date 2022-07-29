using Newtonsoft.Json;
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
    public partial class ItemsPage : ContentPage
    {
        ItemsPageVM viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            viewModel = new ItemsPageVM();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.RefreshList();
            BindingContext = null;
            BindingContext = this.viewModel;
        }

        public ItemsPageVM ViewModel
        {
            get { return BindingContext as ItemsPageVM; }
            set { BindingContext = value; }
        }
    }
}