using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShopSmartDevice.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public double Montant { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public string NumBancaire { get; set; }
        public string Photo { get; set; }
        public string NomClient { get { return Nom + " " + Prenom; } }

        //les noms des produits qui sont transférés du panier
        public string ProduitsStr { get; set; }


        public Facture()
        {
            //icône par défaut pour les factures

            this.Photo = "https://previews.123rf.com/images/arcady31/arcady311510/arcady31151000003/46532249-invoice-icon.jpg";
            this.ProduitsStr= String.Join(", ", App.Panier.GetProductNames()); 

        }



    }
}
