using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace Predmetni_projekat_Formula1
{
    /// <summary>
    /// Interaction logic for ProizvodjacEditWindow.xaml
    /// </summary>
    public partial class ProizvodjacEditWindow : Window
    {
        public Proizvodjac MyProizvodjac { get; set; }
        
        public ProizvodjacEditWindow(Proizvodjac proizvodjac)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.DataContext = proizvodjac;
            MyProizvodjac = proizvodjac;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string? sediste_pre = MyProizvodjac.Sediste;
            string? source_pre = MyProizvodjac.Source;
            var bndExNaziv = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            var bndExSediste = tbSediste.GetBindingExpression(TextBox.TextProperty);
            var bndExSource = tbSource.GetBindingExpression(TextBox.TextProperty);
            bndExNaziv.UpdateSource();
            bndExSediste.UpdateSource();
            bndExSource.UpdateSource();
            if(sediste_pre != MyProizvodjac.Sediste && Owner is MainWindow wnd)
            {
                Drzava? d_new = wnd.Drzave.Where(x => x.Naziv == MyProizvodjac.Sediste).FirstOrDefault();
                Drzava? d_old = wnd.Drzave.Where(x => x.Naziv == sediste_pre).FirstOrDefault();
                if (d_old != null) 
                {
                     d_old.Proizvodjaci.Remove(MyProizvodjac);
                    if (d_old.Proizvodjaci.Count == 0) wnd.Drzave.Remove(d_old);
                } 
                if(d_new != null)
                {
                    d_new.Proizvodjaci.Add(MyProizvodjac);
                }
                else
                {
                    d_new = new Drzava { Naziv = MyProizvodjac.Sediste, Proizvodjaci = new ObservableCollection<Proizvodjac> { MyProizvodjac } };
                    wnd.Drzave.Add(d_new);
                }
            }
            if (!File.Exists(MyProizvodjac.Source))
            {
                MessageBox.Show("Morate izabrati tacnu putanju!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                MyProizvodjac.Source = source_pre;
                return;
            }
            this.Close();
        }

        private void Button_Click_Open(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? opened = openFileDialog.ShowDialog();
            string source = "";
            if (opened == true) 
            {
                source = openFileDialog.FileName;
                if(Owner is MainWindow window)
                {
                    foreach(Drzava d in window.Drzave)
                    {
                        if(d.Proizvodjaci.Where(x => x.Source != null && source.Contains(x.Source.Split('/').Last())).Any())
                        {
                            MessageBox.Show("Ova slika je vec upotrebljena!","Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
                            return;
                        }
                    }
                    tbSource.Text = source;
                }
            }
        }

    }
}
