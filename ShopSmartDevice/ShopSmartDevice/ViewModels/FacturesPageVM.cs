using ShopSmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace ShopSmartDevice.ViewModels
{
    public class FacturesPageVM : BaseViewModel
    {
        private ObservableCollection<Facture> _listFactures;

        //le changement de la liste des factures doit être notifié à la page
        public ObservableCollection<Facture> ListFactures
        {
            get { return _listFactures; }
            set { SetValue(ref _listFactures, value); }
        }

        //définir une commande de suppression pour appeler la méthode de suppression
        public Command Supprimer { get; }

        public FacturesPageVM()
        {
            this.ListFactures = new ObservableCollection<Facture>();
            this.Supprimer = new Command<Facture>(DeleteItem);

        }

        private async void DeleteItem(Facture facture)
        {
            //dialogue pour demander à l'utilisateur de confirmer l'action de suppression

            bool answer = await Application.Current.MainPage.DisplayAlert("Alerte", "Voulez-vous supprimer ce facture?", "Oui", "Non");

            if (answer)
            {
                //appeler la méthode de suppression à partir de la base de données
                await App.dataProviderService.DeleteFactureAsync(facture);
                await Application.Current.MainPage.DisplayAlert("Confirmation", "Facture supprimé avec succès", "OK");
            }
            //recharger la liste
            LoadSmartDevices();
        }

        public void RefreshList()
        {
            LoadSmartDevices();
        }
        private async void LoadSmartDevices()
        {
            try
            {
                this.ListFactures.Clear();
                var items = await App.dataProviderService.GetAllFactureAsync();
                foreach (var item in items)
                {
                    this.ListFactures.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
