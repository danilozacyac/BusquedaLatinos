using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BusquedaLatinos.Dto;
using BusquedaLatinos.Indices;
using BusquedaLatinos.Model;
using ScjnUtilities;
using BusquedaLatinos.Singleton;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using BusquedaLatinos.Formularios;
using MantesisApi.Dto;
using System.Collections.ObjectModel;
using BusquedaLatinos.Impresiones;

namespace BusquedaLatinos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> Alfabeto = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private Terminos selectedTermino = null;
        private TesisDto selectedTesis = null;

        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LstTerminos.DataContext = TerminosSingleton.Terminos;
            
        }

        

        private void BtnIndexa_Click(object sender, RoutedEventArgs e)
        {
             List<TesisIndx> tesis = new TesisModel().GetInfoForIndex();

            MyLucene lucene = new MyLucene(tesis);
            lucene.BuildIndex();

            MessageBox.Show("Indexación terminada");
        }

        private void BtnConIndice_Click(object sender, RoutedEventArgs e)
        {
            MyLucene lucene = new MyLucene();

            TerminosModel model = new TerminosModel();

            //List<Terminos> terminos = model.GetTerminos();

            foreach (Terminos termino in TerminosSingleton.Terminos)
            {
                termino.Iuses = lucene.SearchIuses(termino.Termino.ToLower());

                foreach (int ius in termino.Iuses.Distinct())
                    model.InsertaRelacion(termino, ius);

            }

            //new WordClass(terminos, String.Empty).ImprimeResultados();
        }

        private void BtnSinIndice_Click(object sender, RoutedEventArgs e)
        {
            TesisModel model = new TesisModel();

            ObservableCollection<Terminos> terminos = new TerminosModel().GetTerminos();

            int contador = 1;
            foreach (Terminos termino in terminos)
            {
                termino.Iuses = model.GetIuses(StringUtilities.PrepareToAlphabeticalOrder(termino.Termino));

                Console.WriteLine(contador.ToString());
                contador++;
            }

            //terminos[0].Iuses = model.GetIuses(terminos[0].Termino);
            //new WordClass(new List<Terminos>() { terminos[0] }, String.Empty).ImprimeResultados();

            new WordClass(terminos, String.Empty).ImprimeResultados();
        }

        private void LstTerminos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            selectedTermino = LstTerminos.SelectedItem as Terminos;

            if (selectedTermino != null)
            {
                if (selectedTermino.Iuses == null)
                {
                    new TerminosModel().GetTesisRelacionadas(selectedTermino);

                    if (selectedTermino.Iuses != null && selectedTermino.Iuses.Count > 0)
                    {
                        selectedTermino.Tesis = new TesisModel().GetDetalleTesisRel(selectedTermino.Iuses);
                        LblTotalRelaciones.Content = String.Format("{0} tesis relacionadas", selectedTermino.Tesis.Count());
                    }
                    else
                    {
                        LblTotalRelaciones.Content = "No hay tesis relacionadas con este término";
                    }
                }
                else if(selectedTermino.Tesis != null && selectedTermino.Tesis.Count > 0)
                {
                    LblTotalRelaciones.Content = String.Format("{0} tesis relacionadas", selectedTermino.Tesis.Count());
                }
                else
                {
                    LblTotalRelaciones.Content = "No hay tesis relacionadas con este término";
                }
               
                GTesis.DataContext = selectedTermino.Tesis;
                TxtDefinicion.Text = selectedTermino.Definicion;
                TxtBiblio.Text = selectedTermino.Bibliografia;
            }

        }

        private void SearchTermino_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper().Trim();

            if (!String.IsNullOrEmpty(tempString))
            {
                tempString = StringUtilities.PrepareToAlphabeticalOrder(tempString);

                var temporal = (from n in TerminosSingleton.Terminos
                                where n.TerminoStr.Contains(tempString)
                                select n).ToList();
                LstTerminos.DataContext = temporal;
            }
            else
            {
                LstTerminos.DataContext = TerminosSingleton.Terminos;
            }
        }

        

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            TerminoWin addTerm = new TerminoWin() { Owner = this};
            addTerm.ShowDialog();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTermino == null)
            {
                MessageBox.Show("Primero debes de seleccionar el término que deseas modificar");
                return;
            }

            TerminoWin addTerm = new TerminoWin(selectedTermino) { Owner = this };
            addTerm.ShowDialog();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int index = TerminosSingleton.Terminos.IndexOf(selectedTermino);

            if (selectedTermino == null)
            {
                MessageBox.Show("Para eliminar un término primero debes de seleccionarlo", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(String.Format("¿Estás seguro de eliminar el termino \"{0}\"? ", selectedTermino.Termino), "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                bool complete = new TerminosModel().DeleteFuncionario(selectedTermino);

                if (complete)
                    TerminosSingleton.Terminos.Remove(selectedTermino);
                else
                {
                    MessageBox.Show("No se pudo completar la operación. Favor de intentarlo más tarde");
                }
            }

            LstTerminos.SelectedIndex = index;
        }

        private void GTesis_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UnaTesisWin unaTesisWin = new UnaTesisWin(selectedTermino.Tesis, selectedTermino.Tesis.ToList().IndexOf(selectedTesis), selectedTermino) { Owner = this };
            unaTesisWin.ShowDialog();
        }

        private void GTesis_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            selectedTesis = GTesis.SelectedItem as TesisDto;
        }


        

        private void BtnVolumenes_Click(object sender, RoutedEventArgs e)
        {
            VolumenesWin win = new VolumenesWin() { Owner = this };
            win.ShowDialog();
        }

        private void BtnImprimirLetras_Click(object sender, RoutedEventArgs e)
        {


            SelectLetras select = new SelectLetras(){Owner = this};
            select.ShowDialog();
        }

        private void BtnImprimirTerm_Click(object sender, RoutedEventArgs e)
        {

            if (selectedTermino == null)
            {
                MessageBox.Show("Selecciona el término que deseas imprimir", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            WordPrints print = new WordPrints();
            print.ImprimeSelección(selectedTermino);
        }

        private void BtnImprimirTodo_Click(object sender, RoutedEventArgs e)
        {
            WordPrints prints = new WordPrints(MainWindow.Alfabeto);
            prints.GeneraDocumentoWord();
        }

    }
}
