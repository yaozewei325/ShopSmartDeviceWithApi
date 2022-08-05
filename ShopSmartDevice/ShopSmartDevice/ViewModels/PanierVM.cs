using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShopSmartDevice.ViewModels
{
    public class PanierVM : BaseViewModel
    {

        //les modifications de la quantité totale et du montant du panier seront notifiées à la page
        private int count;
        public int Count
        {
            get { return count; }
            set { SetValue(ref count, value); }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { SetValue(ref total, value); }
        }


        private ObservableCollection<SmartDevice> content;

        public ObservableCollection<SmartDevice> Content
        {
            get { return content; }
            set { SetValue(ref content, value); }
        }

        //définir les délégués de commande
        public Command Paiement { get; }
        public Command ViderPanier { get; }
        public Command Supprimer { get; }
        public PanierVM()
        {
            //la liste des contenus est initialisée à partir du modèle de panier
            this.Content = new ObservableCollection<SmartDevice>(App.Panier.GetContent());

            this.Paiement = new Command(OnPaiement, Validate);
            this.ViderPanier = new Command(ClearList);
            this.Supprimer = new Command<SmartDevice>(DeleteItem);

            // notifie le modèle de vue le changement du Count et Total.

            this.PropertyChanged += (_, __) => Paiement.ChangeCanExecute();


        }

        private async void OnPaiement()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Alerte", "Procédez au paiement?", "Oui", "Non");

            if (answer)
            {
                await Shell.Current.GoToAsync(nameof(PaiementPage));
            }
        }

        private bool Validate()
        {
            // verifier le modèle de vue si la panier est toujours vide, sinon, activé le boutton de paiement.

            return this.Content.Count != 0;
        }

        private async void DeleteItem(SmartDevice item)
        {
            //message de confirmation avant de le supprimer
            bool answer = await Application.Current.MainPage.DisplayAlert("Alerte", "Voulez-vous supprimer ce produit?", "Oui", "Non");

            if (answer)
            {
                App.Panier.RemoveProduct(item);
                await Application.Current.MainPage.DisplayAlert("Confirmation", "Produit supprimé avec succès", "OK");
            }

            //recharger la page à chaque fois que l'on supprime quelque chose

            LoadContents();

        }

        private void ClearList()
        {
            App.Panier.ClearPanier();
            LoadContents();

        }


        public void RefreshList()
        {
            LoadContents();
        }
        private void LoadContents()
        {
            try
            {
                this.Content.Clear();
                var items = App.Panier.GetContent();
                foreach (var item in items)
                {
                    this.Content.Add(item);
                }
                this.Count = App.Panier.CountPanier();
                this.Total = App.Panier.GetTotal();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


        }
    }
}
