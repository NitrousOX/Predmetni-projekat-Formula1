using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predmetni_projekat_Formula1
{
    class Proizvodjac
    {
		private int id;
		private string? naziv;
		private string? sediste;
		private string? source;

		public string? Source
		{
			get { return source; }
			set { source = value; }
		}
		public string? Sediste
		{
			get { return sediste; }
			set { sediste = value; }
		}
		public int Id 
		{
			get { return id; }
			set { id = value; }
		}
		public string? Naziv
		{
			get { return naziv; }
			set { naziv = value; }
		}

	}
}
