using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
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
        private Image temp = new Image();
        public ObservableCollection<Drzava> Drzave
        {
            get { return drzave; }
        }
        public MainWindow()
        {
            InitializeComponent();
            LoadProizvodjace("Proizvodjaci.txt");
            treeView1.DataContext = drzave;
            itemsCtrl.DataContext = proizvodjaciMapa;
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
            Proizvodjac? p = e.Data.GetData(typeof(Proizvodjac)) as Proizvodjac;
            if (sender is not Image imgMap) return;
            if (p != null)
            {
                if(proizvodjaciMapa.Contains(p)) 
                {
                    proizvodjaciMapa.Remove(p);
                } 
                Point loc = e.GetPosition(imgMap);
                p.Location = new Thickness((loc.X - imgMap.Width / 2 + 10) * 2, (loc.Y - imgMap.Height / 2 + 10) * 2, 0, 0 );
                proizvodjaciMapa.Add(p);
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not Image imgProizvodjac) return;
            Proizvodjac? p = null;
            foreach(Drzava d in drzave)
            {
                p = GetProizvodjacFromImage(imgProizvodjac, d.Proizvodjaci);
                if(p != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(imgProizvodjac, p, DragDropEffects.Copy);
                    break;
                }
            }
        }

        private void MenuItem_Click_Ukloni(object sender, RoutedEventArgs e)
        {
            if(temp != null)
            {
                var proizvodjac = GetProizvodjacFromImage(temp, proizvodjaciMapa);
                if(proizvodjac != null)
                {
                    proizvodjaciMapa.Remove(proizvodjac);
                }
            }
        }
        private void MenuItem_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Ovom operacijom cete potpuno obrisati proizvodjaca iz aplikacije!",
                "Oprez!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            {
                return;
            }
            if(temp != null)
            {
                var proizvodjac = GetProizvodjacFromImage(temp, proizvodjaciMapa);
                if(proizvodjac != null)
                {
                    foreach(Drzava d in drzave)
                    {
                        if (d.Proizvodjaci.Contains(proizvodjac))
                        {
                            d.Proizvodjaci.Remove(proizvodjac);
                            break;
                        }
                    }
                    proizvodjaciMapa.Remove(proizvodjac);
                }
            }
        }
        private void MenuItem_Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if(temp != null)
            {
                var proizvodjac = GetProizvodjacFromImage(temp, proizvodjaciMapa);
                if(proizvodjac != null)
                {
                    var prozor = new ProizvodjacEditWindow(proizvodjac);
                    prozor.Owner = this;
                    prozor.ShowDialog();
                }
            }
        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Source is Image image)
            {
                temp = image;
            }
        }
        private void LoadProizvodjace(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach(string line in lines)
                {

                    string[] parts = line.Split(',');
                    Proizvodjac p = new Proizvodjac
                    {
                        Id = idnext++,
                        Naziv = parts[1],
                        Sediste = parts[2],
                        Source = parts[3]
                    };
                    Drzava? d = drzave.Where(x => x.Naziv == parts[2]).FirstOrDefault();
                    if(d == null)
                    {
                        drzave.Add(new Drzava
                        {
                            Naziv = parts[2],
                            Proizvodjaci = new ObservableCollection<Proizvodjac>
                            {
                                p
                            }
                        });
                    }
                    else
                    {
                        if (d.Proizvodjaci.Contains(p))
                        {
                            continue;
                        }
                        else
                        {
                            d.Proizvodjaci.Add(p);
                        }
                    }
                }
            }
        }
        private static string GetLastPartOfSource(Image img)
        {
            return img.Source.ToString().Split('/').Last();
        }

        public static Proizvodjac? GetProizvodjacFromImage(Image img, ICollection<Proizvodjac> proizvodjaci)
        {
            return proizvodjaci.Where(x => x.Source != null && x.Source.Contains(GetLastPartOfSource(img))).FirstOrDefault();
        }
    }
}
