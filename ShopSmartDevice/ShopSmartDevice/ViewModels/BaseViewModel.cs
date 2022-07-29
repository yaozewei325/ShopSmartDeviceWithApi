using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ShopSmartDevice
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //Définir un modèle de modèle de vue dont les modèles de vue doivent hériter pour éviter la
        //duplication de la méthode de PropertyChanged et avoir des codes plus propres.

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //une méthode  modèle  générique pour tous les setters de propriétés 
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            //si la valeur est changée, mettre la nouvelle valeur et notifier la page, sinon retourner. 

            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;
            backingField = value;

            OnPropertyChanged(propertyName);
        }
    }
}
