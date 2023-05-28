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
            this.ResizeMode = ResizeMode.NoResize;
            this.DataContext = proizvodjac;
            MyProizvodjac = proizvodjac;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bndExNaziv = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            var bndExSediste = tbSediste.GetBindingExpression(TextBox.TextProperty);
            var bndExSource = tbSource.GetBindingExpression(TextBox.TextProperty);
            bndExNaziv.UpdateSource();
            bndExSediste.UpdateSource();
            bndExSource.UpdateSource();
            this.Close();
        }

        private void Button_Click_Open(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? opened = openFileDialog.ShowDialog();

            if (opened == true) 
            {
                tbSource.Text = openFileDialog.FileName;
            }
        }
    }
}
