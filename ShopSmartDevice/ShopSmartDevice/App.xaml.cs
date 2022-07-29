using ShopSmartDevice.Data;
using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopSmartDevice
{
    public partial class App : Application
    {
        public static DataProviderService dataProviderService = new DataProviderService();

        //la déclaration du panier dans App pour faciliter l'appel dans les autres modèles de vue
        public static Panier _panier;

        // la declaration de dbContext comme variable statique pour faciliter l'appel dans les autres modèles de vue
    


        //Methodes d'acces
        public static Panier Panier
        {
            get { return _panier; }
        }
        

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
          
            _panier = new Panier();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
