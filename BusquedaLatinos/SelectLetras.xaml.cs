using BusquedaLatinos.Impresiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace BusquedaLatinos
{
    /// <summary>
    /// Interaction logic for SelectLetras.xaml
    /// </summary>
    public partial class SelectLetras
    {

        List<CheckBox> myChecks = new List<CheckBox>();
 
        public SelectLetras()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string letra in MainWindow.Alfabeto)
            {
                CheckBox check = new CheckBox();
                check.Name = String.Format("{0}{1}", "MyCheck", letra);
                check.Content = "   " + letra;
                check.Tag = letra;
                check.FontSize = 18;
                check.Margin = new Thickness(10);

                myChecks.Add(check);

                PanelLetras.Children.Add(check);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            List<string> letrasPrint = new List<string>();

            foreach (CheckBox check in myChecks)
            {
                if (check.IsChecked == true)
                {
                    letrasPrint.Add(check.Tag.ToString());
                }
            }

            if(letrasPrint.Count == 0)
            {
                MessageBox.Show("Debes seleccionar al menos una letra para imprimir", "Atención", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WordPrints prints = new WordPrints(letrasPrint);
            prints.GeneraDocumentoWord();
            this.Close();
        }
    }
}
