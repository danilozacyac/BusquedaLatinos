using BusquedaLatinos.Dto;
using BusquedaLatinos.Model;
using BusquedaLatinos.Singleton;
using ScjnUtilities;
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

namespace BusquedaLatinos.Formularios
{
    /// <summary>
    /// Interaction logic for TerminoWin.xaml
    /// </summary>
    public partial class TerminoWin
    {

        private Terminos termino;
        private bool isUpdate = false;


        public TerminoWin()
        {
            InitializeComponent();
            termino = new Terminos();
        }

        public TerminoWin(Terminos termino)
        {
            InitializeComponent();
            this.termino = termino;
            this.isUpdate = true;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LblTermino.Content = String.Format("Término por {0}", (isUpdate) ? "actualizar" : "agregar");

            if (isUpdate)
            {
                TxtTermino.Text = termino.Termino;
                TxtDefinicion.Text = termino.Definicion;
                TxtBibliografia.Text = termino.Bibliografia;
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            termino.Termino = TxtTermino.Text;
            termino.TerminoStr = StringUtilities.PrepareToAlphabeticalOrder(termino.Termino);
            termino.Definicion = TxtDefinicion.Text;
            termino.Bibliografia = TxtBibliografia.Text;

            if (isUpdate)
            {
                bool complete = new TerminosModel().UpdateTermino(termino);

                if (complete)
                    this.Close();
                else
                    MessageBox.Show("No se pudo completar la operación, favor de volver a intentarlo", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                bool complete = new TerminosModel().InsertaTermino(termino);

                if (complete)
                {
                    TerminosSingleton.Terminos.Add(termino);
                    this.Close();
                }
                else
                    MessageBox.Show("No se pudo completar la operación, favor de volver a intentarlo", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
         
        }
    }
}
