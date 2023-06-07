using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Predmetni_projekat_Formula1
{
    /// <summary>
    /// Interaction logic for VozacEditWindow.xaml
    /// </summary>
    public partial class VozacEditWindow : Window
    {
        public Vozac? GetVozac { get; set; }
        public VozacEditWindow(Vozac vozac)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            GetVozac = vozac;
            Edit_TB_ID.Text = GetVozac.ID.ToString();
            Edit_TB_Name.Text = GetVozac?.First_Name;
            Edit_TB_Last_Name.Text = GetVozac?.Last_Name;
            Edit_TB_Team.Text = GetVozac?.Team;
            Edit_TB_Nationality.Text = GetVozac?.Nationality;
            Edit_TB_Chassis.Text = GetVozac?.Chassis_Number;
            Edit_TB_Num_Races.Text = GetVozac?.Num_Races.ToString();
            Edit_TB_Num_Wins.Text = GetVozac?.Num_Wins.ToString();
            Edit_TB_Path.Text = GetVozac?.Picture_path;


        }

        private void BTN_Browse_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? opened = openFileDialog.ShowDialog();
            string source = "";
            if (opened == true)
            {
                source = System.IO.Path.GetRelativePath(Directory.GetCurrentDirectory(), openFileDialog.FileName);
                if (Owner is MainWindow window)
                {
                    foreach (Vozac vozac in window.Vozaci)
                    {
                        if (vozac.Picture_path == source)
                        {
                            MessageBox.Show("Ova slika je vec upotrebljena!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    Edit_TB_Path.Text = source;
                }
            }
        }


        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Edit_TB_Name.Text == String.Empty || Edit_TB_Last_Name.Text == String.Empty || 
            Edit_TB_Team.Text == String.Empty || Edit_TB_Nationality.Text == String.Empty || 
            Edit_TB_Chassis.Text == String.Empty || Edit_TB_Num_Races.Text == String.Empty ||
            Edit_TB_Num_Wins.Text == String.Empty || Edit_TB_Path.Text == String.Empty){
                MessageBox.Show("Morate popuniti sva polja!");
            }else{
                if (Edit_Button.Content.ToString() == "Dodaj" && Owner is MainWindow wnd)
                {
                    wnd.Vozaci.Add(new Vozac(uint.Parse(Edit_TB_ID.Text), Edit_TB_Name.Text, Edit_TB_Last_Name.Text, Edit_TB_Team.Text, Edit_TB_Nationality.Text, Edit_TB_Chassis.Text, int.Parse(Edit_TB_Num_Races.Text), int.Parse(Edit_TB_Num_Wins.Text), Edit_TB_Path.Text));
                    Close();
                }
                else
                {
                    if (Owner is MainWindow wd)
                        foreach(Vozac voz in wd.Vozaci)
                        {
                            if(voz.ID.ToString() == Edit_TB_ID.Text)
                            {
                                voz.First_Name = Edit_TB_Name.Text;
                                voz.Last_Name = Edit_TB_Last_Name.Text;
                                voz.Team = Edit_TB_Team.Text;
                                voz.Nationality = Edit_TB_Nationality.Text;
                                voz.Chassis_Number = Edit_TB_Chassis.Text;
                                voz.Num_Races = int.Parse(Edit_TB_Num_Races.Text);
                                voz.Num_Wins = int.Parse(Edit_TB_Num_Wins.Text);
                                voz.Picture_path = Edit_TB_Path.Text;
                                Close();
                                break;
                            }
                        }
                }

                }
        }

        private void Edit_Provera_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (!textbox.Text.All(Char.IsLetter))
                {
                    MessageBox.Show("Morate uneti samo slova!");
                    textbox.Text = String.Empty;
                }
            }
        }
        private void Edit_Provera_Number_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (!textbox.Text.All(Char.IsDigit))
                {
                    MessageBox.Show("Morate uneti samo brojeve!");
                    textbox.Text = String.Empty;
                }
            }
        }

        private void Edit_TB_Chassis_LostFocus(object sender, RoutedEventArgs e)
        {     
            foreach (Char ch in Edit_TB_Chassis.Text)
            {
                if(!Char.IsLetterOrDigit(ch) && ch != '_')
                {
                    MessageBox.Show("Morate uneti samo brojeve ili slova!");
                    Edit_TB_Chassis.Text = String.Empty;
                }
                
            }
            

        }


    }
}
