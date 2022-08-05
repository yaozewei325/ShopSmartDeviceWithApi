using ShopSmartDevice.Data;
using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShopSmartDevice.ViewModels
{
    internal class GestionItemPageVM : BaseViewModel
    {
        private SmartDevice deviceItem;
        public string txtDeviceId { get; set; }
        public int DeviceId { get; set; }

        public string txtModele { get; set; }
        public string txtFabriquant { get; set; }

        public string txtType { get; set; }
        public string txtPlateforme { get; set; }
        public double Prix { get; set; }
        public string txtPrix { get; set; }
        public string txtImageUrl { get; set; }

        public SmartDevice DeviceItem { get { return deviceItem; } set { SetValue(ref deviceItem, value); } }
        public Command GetDeviceByIdCmd { get; set; }
        public Command UpdateDeviceCmd { get; set; }

        public GestionItemPageVM()
        {

            GetDeviceByIdCmd = new Command(GetDeviceById);
            UpdateDeviceCmd = new Command(UpdateDevice);
            this.txtImageUrl = "https://fdn2.gsmarena.com/vv/bigpic/samsung-galaxy-a32-5g.jpg";



        }

        //saisir l’id inséré dans la zone de texte pour trouver le device

        private async void GetDeviceById(object obj)
        {
            DeviceId = Int32.Parse(txtDeviceId);
            if (DeviceId == 0) return;
            DeviceItem = new SmartDevice();
            DeviceItem = await App.dataProviderService.GetDeviceById(DeviceId);
        }

        //saisir les valeurs insérés dans la zone de texte pour modifier l’élément
        private async void UpdateDevice()
        {
            Prix = Double.Parse(txtPrix, System.Globalization.NumberStyles.Currency);
            await App.dataProviderService.UpdateDeviceAsync(DeviceId, txtModele, txtFabriquant, txtType, txtPlateforme, Prix, txtImageUrl);
            await Shell.Current.GoToAsync(nameof(ItemsPage));
        }
    }
}
