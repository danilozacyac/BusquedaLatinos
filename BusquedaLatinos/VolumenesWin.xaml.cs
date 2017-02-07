using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusquedaLatinos.Indices;
using BusquedaLatinos.Model;
using BusquedaLatinos.Dto;
using BusquedaLatinos.Singleton;

namespace BusquedaLatinos
{
    /// <summary>
    /// Interaction logic for Volumenes.xaml
    /// </summary>
    public partial class VolumenesWin
    {
        Volumen selectedVolumen;

        public VolumenesWin()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxVolumen.DataContext = new VolumenModel().GetVolumenesForCombo();
        }

        private void CbxVolumen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedVolumen = CbxVolumen.SelectedItem as Volumen;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVolumen == null)
            {
                MessageBox.Show("Para continuar debes seleccionar el volumen que quieres agregar");
                return;
            }

            List<TesisIndx> tesis = new TesisModel().GetInfoForIndex(selectedVolumen.Volumen1);

            MyLucene lucene = new MyLucene(tesis);
            lucene.BuildIndex();


            TerminosModel model = new TerminosModel();

            foreach (Terminos termino in TerminosSingleton.Terminos)
            {
                termino.Iuses = lucene.SearchIuses(termino.Termino.ToLower());

                foreach (int ius in termino.Iuses.Distinct())
                    model.InsertaRelacion(termino, ius);

            }


            MessageBox.Show("Indexación terminada");
        }
    }
}
