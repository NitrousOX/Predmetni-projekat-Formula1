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
        private ObservableCollection<Proizvodjac> proizvodjaciMapa = new ObservableCollection<Proizvodjac>();
        public MainWindow()
        {
            InitializeComponent();
            var drzava = new Drzava { Naziv = "Nemacka" };
            drzava.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Mercedes", Source = "/Slike/mercedes.jpg"});
            drzava.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Audi", Source = "/Slike/audi.jpg" });
            drzava.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "BMW", Source = "/Slike/bmw.jpg" });

            var drzava2 = new Drzava { Naziv = "Francuska" };
            drzava2.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Peugeot", Source = "/Slike/peugeot.jpg" });
            drzava2.Proizvodjaci.Add(new Proizvodjac { Id = idnext++, Naziv = "Citroen", Source = "/Slike/citroen.jpg" });

            drzave.Add(drzava);
            drzave.Add(drzava2);
            treeView1.DataContext = drzave;
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not TextBlock textBlock) return;

            Proizvodjac? p = null;
            foreach(Drzava d in drzave)
            {
                p = d.Proizvodjaci.Where(x => x.Naziv == textBlock.Text).FirstOrDefault();
                if(p != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(textBlock, p, DragDropEffects.Copy); 
                    break;
                }
            }

        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            //TODO: sacuvaj proizvodjaca u coleksciju za sada...
        }
    }
}
