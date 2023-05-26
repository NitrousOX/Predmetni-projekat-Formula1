using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predmetni_projekat_Formula1
{
    class Proizvodjac : INotifyPropertyChanged
    {
		private int id;
		private string? naziv;
		private string? sediste;
		private string? source;

		public string? Source
		{
			get { return source; }
			set 
			{
				if(value != source)
				{
					source = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));
				}
			}
		}
		public string? Sediste
		{
			get { return sediste; }
			set 
			{
				if(value != sediste)
				{
					sediste = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sediste)));
				}
			}
		}
		public int Id 
		{
			get { return id; }
			set 
			{
				if(value != id)
				{
					id = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
				}
			}
		}
		public string? Naziv
		{
			get { return naziv; }
			set 
			{
				if(value != naziv)
				{
					naziv = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Naziv)));
				}
			}
		}

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
