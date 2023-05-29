using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predmetni_projekat_Formula1
{
    public class Drzava : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Proizvodjac> Proizvodjaci { get; } = new ObservableCollection<Proizvodjac>();
        private string? naziv;

        public string Naziv
        {
            get { if (naziv != null) return naziv; else return ""; }
            set
            {
                if(naziv != value)
                {
                    naziv = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Naziv)));
                }
            } 
        }

    }
}
