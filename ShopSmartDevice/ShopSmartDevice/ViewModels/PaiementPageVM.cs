using ShopSmartDevice.Models;
using ShopSmartDevice.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace ShopSmartDevice.ViewModels
{
    public class PaiementPageVM : BaseViewModel
    {
        private string _ajouterNom;
        private string _ajouterAdresse;
        private string _ajouterTele;
        private string _ajouterCourriel;
        private string _ajouterNumBancaire;


        //parce que le bouton de paiement est conditionnel, chaque fois que le changement de
        //valeur dans la zone de texte du nom et du prénom doit être notifié dans les deux sens.
        public string AjouterNom
        {
            get { return _ajouterNom; }
            set
            {
                SetValue(ref _ajouterNom, value);
            }
        }

        private string _ajouterPrenom;
        public string AjouterPrenom
        {
            get { return _ajouterPrenom; }
            set { SetValue(ref _ajouterPrenom, value); }
        }

        public string AjouterAdresse
        {
            get { return _ajouterAdresse; }
            set { SetValue(ref _ajouterAdresse, value); }
        }
        public string AjouterTele
        {
            get { return _ajouterTele; }
            set { SetValue(ref _ajouterTele, value); }
        }
        public string AjouterCourriel
        {
            get { return _ajouterCourriel; }
            set { SetValue(ref _ajouterCourriel, value); }
        }
        public string AjouterNumBancaire
        {
            get { return _ajouterNumBancaire; }
            set { SetValue(ref _ajouterNumBancaire, value); }
        }

        public double Montant { get; set; }


        //Declaration du delegate de command
        public Command CmdAjouter { get; private set; }

        public Command CmdCancel { get; private set; }


        public PaiementPageVM()
        {
            CmdAjouter = new Command(async () => await AjouterFactureAsync(), ValidateSave);

            CmdCancel = new Command(OnCancel);

            //  a chaque fois que l'un des texbox change de valeur (execute la methode validateSave)
            this.PropertyChanged += (_, __) => CmdAjouter.ChangeCanExecute();

            //le montant est transféré du panier
            this.Montant = App.Panier.GetTotal();

        }

        private bool ValidateSave()
        {
            //le bouton de paiement ne sera activé que si le nom et le prénom sont saisis


            return !string.IsNullOrWhiteSpace(AjouterNom)
               && !string.IsNullOrWhiteSpace(AjouterPrenom)
               && !string.IsNullOrWhiteSpace(AjouterAdresse)
               && !string.IsNullOrWhiteSpace(AjouterTele)
               && !string.IsNullOrWhiteSpace(AjouterCourriel)
               && !string.IsNullOrWhiteSpace(AjouterNumBancaire);

            //&& Regex.IsMatch(this.AjouterTele, @"^[0-9]{3}\s?[0-9]{3}(\s|-)?[0-9]{4}$")
            //&& Regex.IsMatch(this.AjouterCourriel, @"^[a-zA-Z]+\w+@\w+.[a-zA-Z]{2,4}$")
            //&& Regex.IsMatch(this.AjouterNumBancaire, @"^\d{14}$"); 
        }



        //definition d'une action ajouter
        public async Task AjouterFactureAsync()
        {
          
            //Créer un objet facture  
            Facture newFacture = new Facture()
            {

                Nom = AjouterNom,
                Prenom = AjouterPrenom,
                Adresse = AjouterAdresse,
                Telephone = AjouterTele,
                Courriel = AjouterCourriel,
                NumBancaire = AjouterNumBancaire,
                Montant = this.Montant,


            };

            //message pour que l'utilisateur confirme le paiement
            bool answer = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alerte", "Confirmez le paiement?", "Oui", "Non");

            if (answer)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Confirmation", "Paiement traité, directez-vous sur la page des factures", "OK");

                //lors de la confirmation du paiement, Ajouter la nouvelle facture au base de donée.

                await App.dataProviderService.AddFactureAsync(newFacture);
                App.Panier.ClearPanier();

                //rediriger vers la page des factures
                await Shell.Current.GoToAsync(nameof(FacturesPage));
            }
        }

        //En cliquant sur le bouton d'annulation, rediriger vers la page d'accueil.
        private async void OnCancel(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AccueilPage));

        }

    }
}
