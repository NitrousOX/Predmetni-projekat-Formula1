using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
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
    /// 


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            List<Vozac> vozaci = Ucitaj("C:\\Users\\poljv\\Documents\\GitHub\\Predmetni-projekat-Formula1\\vozaci.txt");
            TxtBox_Pretraga.Text = vozaci[0].ToString();
        }

        private void Btn_Export_Click(object sender, RoutedEventArgs e)
        {
            if ((Radio_CSV.IsChecked == false) && (Radio_XLS.IsChecked == false))
            {
                MessageBox.Show("Morate izabrati format");
            }
            if(Radio_CSV.IsChecked == true)
            {

            }
            else
            {

            }
        }


        List<Vozac> Ucitaj(string putanja)
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
                first_Name = items[0];
                last_Name = items[1];
                team = items[2];
                nationality = items[3];
                chassis_Number = items[4];
                num_Races = int.Parse(items[5]);
                num_Wins = int.Parse(items[6]);
                picture_path = "C:\\Users\\poljv\\Documents\\GitHub\\Predmetni-projekat-Formula1\\Slike\\" + items[7];

                vozaci.Add(new Vozac(id, first_Name, last_Name, team, nationality, chassis_Number, num_Races, num_Wins, picture_path));
            }

            return vozaci;
        }
    }
}
