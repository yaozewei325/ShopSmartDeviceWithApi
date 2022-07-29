using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ShopSmartDevice.Models
{
    public class SmartDevice
    {
        //definir les enums pour faciliter les contraintes

 

        public int Id { get; set; }
        public string Modele { get; set; }
        public string Fabriquant { get; set; }

        public string Type { get; set; }
        public string Plateforme { get; set; }
        public double Prix { get; set; }
        public string ImageUrl { get; set; }
       



    }
}
