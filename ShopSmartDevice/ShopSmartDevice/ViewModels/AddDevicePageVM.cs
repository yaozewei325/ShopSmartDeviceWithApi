using Acr.UserDialogs;
using ShopSmartDevice.Data;
using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShopSmartDevice.ViewModels
{
    internal class AddDevicePageVM
    {
        private readonly DataProviderService dataService = new DataProviderService();

        public string txtModele { get; set; }
        public string txtFabriquant { get; set; }

        public string txtType { get; set; }
        public string txtPlateforme { get; set; }
        public double Prix { get; set; }
        public string txtPrix { get; set; }
        public string txtImageUrl { get; set; }


        public Command AddDeviceCmd { get; set; }

        public AddDevicePageVM()
        {
            AddDeviceCmd = new Command(AddDevice);
            this.txtImageUrl = "https://fdn2.gsmarena.com/vv/bigpic/samsung-galaxy-a32-5g.jpg";
        }

        private async void AddDevice()
        {
            Prix = Double.Parse(txtPrix, System.Globalization.NumberStyles.Currency);

            SmartDevice newDevice = new SmartDevice()
            {
                Modele = txtModele,
                Fabriquant = txtFabriquant,
                Type = txtType,
                Plateforme = txtPlateforme,
                Prix = Prix,
                ImageUrl = txtImageUrl,

            };
            SmartDevice device = await App.dataProviderService.AddDeviceAsync(newDevice);
            if (device == null)

                await App.Current.MainPage.DisplayAlert("Confirmation", $"Élément ajouté avec Id = {device.Id}", "Fermer");


            else
                await App.Current.MainPage.DisplayAlert("Confirmation", $"Élément ajouté avec Id = {device.Id}", "Fermer");

            // revenir à la page List
            await Shell.Current.GoToAsync(nameof(ItemsPage));

        }



    }
}

