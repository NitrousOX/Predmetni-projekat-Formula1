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
    /// Interaction logic for ProizvodjacEditWindow.xaml
    /// </summary>
    public partial class ProizvodjacEditWindow : Window
    {
        public Proizvodjac MyProizvodjac { get; set; }
        
        public ProizvodjacEditWindow(Proizvodjac proizvodjac)
        {
            InitializeComponent();
            this.DataContext = proizvodjac;
            MyProizvodjac = proizvodjac;
        }
    }
}
