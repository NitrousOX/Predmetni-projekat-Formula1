using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Predmetni_projekat_Formula1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int idnext = 0;
        private ObservableCollection<Drzava> drzave = new ObservableCollection<Drzava>();
        public MainWindow()
        {
            InitializeComponent();
            var drzava = new Drzava { Naziv = "Nemacka" };
            drzava.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Mercedes" });
            drzava.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Audi" });
            drzava.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "BMW" });

            var drzava2 = new Drzava { Naziv = "Francuska" };
            drzava2.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Peugeot" });
            drzava2.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Citroen" });

            drzave.Add(drzava);
            drzave.Add(drzava2);
            //UcitajProizvodjace("/Proizvodjaci.txt");
            treeView1.DataContext = drzave;
        }

        /*private void UcitajProizvodjace(string fajl)
        {
            if (File.Exists(fajl))
            {
                string[] lines = File.ReadAllLines(fajl);
                foreach (string line in lines)
                {
                    string[] delovi = line.Split(',');
                    var novProizvodjac = new Proizvodjac() { Id = int.Parse(delovi[0]),Naziv = delovi[1], Sediste = delovi[2], Source = delovi[3] };
                    proizvodjaciObsLeft.Add(novProizvodjac);
                    idnext++;
                }
            }
            else
            {
                Console.WriteLine("File {0} does not exist!", fajl);
            }
        }*/
    }
}
