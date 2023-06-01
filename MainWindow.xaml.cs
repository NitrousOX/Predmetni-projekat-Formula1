using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;

namespace Predmetni_projekat_Formula1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private int idnext;
        private ObservableCollection<Drzava> drzave = new ObservableCollection<Drzava>();
        private ObservableCollection<Proizvodjac> proizvodjaciMapa = new ObservableCollection<Proizvodjac>();
        private Image temp = new Image();
        private List<Vozac> lista_vozaca;
        public ObservableCollection<Drzava> Drzave
        {
            get { return drzave; }
        }
        public MainWindow()
        {
            InitializeComponent();//this.ResizeMode = ResizeMode.NoResize;
            LoadProizvodjace("Proizvodjaci.txt");
            treeView1.DataContext = drzave;
            itemsCtrl.DataContext = proizvodjaciMapa;
            lista_vozaca = Ucitaj_Vozace("..\\..\\..\\vozaci.txt");
            VozaciDG.DataContext = lista_vozaca;
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not TextBlock textBlock) return;

            Proizvodjac? p = null;
            foreach (Drzava d in drzave)
            {
                p = d.Proizvodjaci.Where(x => x.Naziv == textBlock.Text).FirstOrDefault();
                if (p != null && e.LeftButton == MouseButtonState.Pressed)
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
                if (proizvodjaciMapa.Contains(p))
                {
                    proizvodjaciMapa.Remove(p);
                }
                Point loc = e.GetPosition(imgMap);
                p.Left = loc.X - 10;
                p.Top = loc.Y - 10;
                proizvodjaciMapa.Add(p);
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not Image imgProizvodjac) return;
            Proizvodjac? p = null;
            foreach (Drzava d in drzave)
            {
                p = GetProizvodjacFromImage(imgProizvodjac, d.Proizvodjaci);
                if (p != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(imgProizvodjac, p, DragDropEffects.Copy);
                    break;
                }
            }
        }

        private void MenuItem_Click_Dodaj(object sender, RoutedEventArgs e)
        {
            Proizvodjac nov_proizvodjac = new Proizvodjac();
            nov_proizvodjac.Id = idnext++;
            var dodaj_wnd = new ProizvodjacEditWindow(nov_proizvodjac);
            dodaj_wnd.Owner = this;
            dodaj_wnd.Title = "Dodaj novog proizvodjaca";
            dodaj_wnd.btnDodajIzmeni.Content = "Dodaj";
            dodaj_wnd.ShowDialog();
        }
        private void MenuItem_Click_Ukloni(object sender, RoutedEventArgs e)
        {
            if (temp != null)
            {
                var proizvodjac = GetProizvodjacFromImage(temp, proizvodjaciMapa);
                if (proizvodjac != null)
                {
                    proizvodjaciMapa.Remove(proizvodjac);
                }
            }
        }
        private void MenuItem_Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ovom operacijom cete potpuno obrisati proizvodjaca iz aplikacije!",
                "Oprez!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            {
                return;
            }
            if (temp != null)
            {
                var proizvodjac = GetProizvodjacFromImage(temp, proizvodjaciMapa);
                if (proizvodjac != null)
                {
                    foreach (Drzava d in drzave)
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
            if (temp != null)
            {
                var proizvodjac = GetProizvodjacFromImage(temp, proizvodjaciMapa);
                if (proizvodjac != null)
                {
                    var prozor = new ProizvodjacEditWindow(proizvodjac);
                    prozor.Owner = this;
                    prozor.Title = "Izmeni proizvodjaca";
                    prozor.btnDodajIzmeni.Content = "Izmeni";
                    prozor.ShowDialog();
                }
            }
        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Image image)
            {
                temp = image;
            }
        }
        private void SaveProizvodjace(string fileName)
        {
            List<string> proizvodjaci = new List<string>();
            foreach (Drzava d in drzave)
            {
                foreach (Proizvodjac p in d.Proizvodjaci)
                {
                    bool naMapi = proizvodjaciMapa.Contains(p);
                    proizvodjaci.Add(String.Format("{0},{1},{2},{3},{4},{5},{6}", p.Id, p.Naziv, p.Sediste, p.Source, p.Left, p.Top, naMapi));

                }
            }
            File.WriteAllLines(fileName, proizvodjaci.ToArray());
        }
        private void LoadProizvodjace(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    foreach (string line in lines)
                    {

                        string[] parts = line.Split(',');
                        Proizvodjac p = new Proizvodjac
                        {
                            Id = int.Parse(parts[0]),
                            Naziv = parts[1],
                            Sediste = parts[2],
                            Source = parts[3],
                            Left = double.Parse(parts[4]),
                            Top = double.Parse(parts[5])
                        };
                        Drzava? d = drzave.Where(x => x.Naziv == parts[2]).FirstOrDefault();
                        if (d == null)
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
                        if (bool.Parse(parts[6]))
                        {
                            proizvodjaciMapa.Add(p);
                        }
                        idnext = p.Id + 1;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry but there was an error with the save file!", "Sorry!", MessageBoxButton.OK, MessageBoxImage.Information);
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

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveProizvodjace("Proizvodjaci.txt");
            base.OnClosing(e);
        }
        private void Btn_Export_Click(object sender, RoutedEventArgs e)
        {
            if ((Radio_CSV.IsChecked == false) && (Radio_XLS.IsChecked == false))
            {
                MessageBox.Show("Morate izabrati format!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (Radio_CSV.IsChecked == true)
            {
                var promptWnd = new SaveFileDialog();
                promptWnd.FileName = "Document.csv";
                promptWnd.DefaultExt = ".csv";
                promptWnd.Filter = "CSV Documents (.csv) |*.csv";
                bool? hasOpened = promptWnd.ShowDialog();
                if (hasOpened == true)
                {
                    try
                    {
                        ExportAsCSV(VozaciDG.DataContext as ICollection<Vozac> ,promptWnd.FileName);
                        MessageBox.Show("File exported succesfuly!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not export file!", "Fatal Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

            }
            else
            {
                var promptWnd = new SaveFileDialog();
                promptWnd.FileName = "Document.xls";
                promptWnd.DefaultExt = ".xls";
                promptWnd.Filter = "XLS Documents (.xls) |*.xls";
                bool? hasOpened = promptWnd.ShowDialog();
                if(hasOpened == true)
                {
                    try
                    {
                        ExportAsXLS(VozaciDG.DataContext as ICollection<Vozac>, promptWnd.FileName);
                        MessageBox.Show("File exported succesfuly!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not export file!", "Fatal Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
        }
        List<Vozac> Ucitaj_Vozace(string putanja)
        {
            List<Vozac> vozaci = new List<Vozac>();
            uint id;
            string first_Name;
            string last_Name;
            string team;
            string nationality;
            string chassis_Number;
            int num_Races;
            int num_Wins;
            string picture_path;

            StreamReader reader = File.OpenText(putanja);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');
                id = uint.Parse(items[0]);
                first_Name = items[1];
                last_Name = items[2];
                team = items[3];
                nationality = items[4];
                chassis_Number = items[5];
                num_Races = int.Parse(items[6]);
                num_Wins = int.Parse(items[7]);
                picture_path = "..\\..\\..\\Slike\\" + items[8];

                vozaci.Add(new Vozac(id, first_Name, last_Name, team, nationality, chassis_Number, num_Races, num_Wins, picture_path));
            }

            return vozaci;
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string searchPrompt = TxtBox_Pretraga.Text;
            ObservableCollection<Vozac> novi_vozaci =  
                new ObservableCollection<Vozac>(lista_vozaca.Where(x => x.First_Name.ToUpper().Contains(searchPrompt.ToUpper()) ||
                                                                        x.Last_Name.ToUpper().Contains(searchPrompt.ToUpper()) ||
                                                                        x.Team.ToUpper().Contains(searchPrompt.ToUpper())));
            VozaciDG.DataContext = novi_vozaci;
        }

        public static void ExportAsCSV(ICollection<Vozac> vozaci, string putanja)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream(putanja, FileMode.Create, FileAccess.Write), Encoding.UTF8);
            streamWriter.WriteLine("ID,First Name,Last Name,Team,Nationality,Chassis Number,Number of races,Number of wins");
            foreach(Vozac v in vozaci)
            {
                streamWriter.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", v.ID, v.First_Name, v.Last_Name, v.Team, v.Nationality, v.Chassis_Number, v.Num_Races, v.Num_Wins));
            }
            streamWriter.Close();
        }

        public static void ExportAsXLS(ICollection<Vozac> vozaci, string putanja)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet? worksheet = workbook.CreateSheet("Sheet1") as HSSFSheet;
            int rowindex = 0;
            HSSFRow? header = worksheet?.CreateRow(rowindex) as HSSFRow;
            if(header != null)
            {
                header.CreateCell(0).SetCellValue("ID");
                header.CreateCell(1).SetCellValue("First Name");
                header.CreateCell(2).SetCellValue("Last Name");
                header.CreateCell(3).SetCellValue("Team");
                header.CreateCell(4).SetCellValue("Nationality");
                header.CreateCell(5).SetCellValue("Chassis number");
                header.CreateCell(6).SetCellValue("Number of races");
                header.CreateCell(7).SetCellValue("Number of wins");

                foreach(Vozac v in vozaci)
                {
                    rowindex++;
                    HSSFRow? dataRow = worksheet?.CreateRow(rowindex) as HSSFRow;
                    if(dataRow != null)
                    {
                        dataRow.CreateCell(0).SetCellValue(v.ID);
                        dataRow.CreateCell(1).SetCellValue(v.First_Name);
                        dataRow.CreateCell(2).SetCellValue(v.Last_Name);
                        dataRow.CreateCell(3).SetCellValue(v.Team);
                        dataRow.CreateCell(4).SetCellValue(v.Nationality);
                        dataRow.CreateCell(5).SetCellValue(v.Chassis_Number);
                        dataRow.CreateCell(6).SetCellValue(v.Num_Races);
                        dataRow.CreateCell(7).SetCellValue(v.Num_Wins);
                    }
                    else
                    {
                        throw new Exception();
                    }

                    using(FileStream fs = new FileStream(putanja, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }
                }

            }
            else
            {
                throw new Exception();
            }


        }
    }
}
