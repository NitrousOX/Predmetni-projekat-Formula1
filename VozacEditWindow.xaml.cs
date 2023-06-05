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

namespace Predmetni_projekat_Formula1
{
    /// <summary>
    /// Interaction logic for VozacEditWindow.xaml
    /// </summary>
    public partial class VozacEditWindow : Window
    {
        public Vozac GetVozac { get; set; }
        public VozacEditWindow(Vozac vozac)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            GetVozac = vozac;
            Edit_TB_ID.Text = GetVozac.ID.ToString();
            Edit_TB_Name.Text = GetVozac?.First_Name;
            Edit_TB_Last_Name.Text = GetVozac?.Last_Name;
            Edit_TB_Team.Text = GetVozac?.Team;
            Edit_TB_Chassis.Text = GetVozac?.Chassis_Number;
            
            Edit_TB_Num_Races.Text = GetVozac?.Num_Races.ToString();
            Edit_TB_Num_Races.Text = GetVozac?.Num_Wins.ToString();
            Edit_TB_Path.Text = GetVozac?.Picture_path;
        }

    }
}
