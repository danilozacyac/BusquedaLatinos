using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BusquedaLatinos.Controllers;
using BusquedaLatinos.Indices;
using MantesisApi.Dto;
using Telerik.Windows.Controls;
using BusquedaLatinos.Dto;

namespace BusquedaLatinos
{
    /// <summary>
    /// Interaction logic for UnaTesisWin.xaml
    /// </summary>
    public partial class UnaTesisWin
    {
        private UnaTesisWinController controller;
        public ObservableCollection<TesisDto> ListaTesis;
        public int PosActual;
        private TesisDto tesisMostrada;
        public List<int> tesisAbiertas;
        public int ColorFondo;
        public Terminos terminoSeleccionado;

        public UnaTesisWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Muestra el detalle de tesis con la posibilidad de navegar a traves de un listado
        /// </summary>
        /// <param name="listaTesis">Lista de Tesis mostrada en la ventana principal</param>
        /// <param name="posActual">Posición de la tesis seleccionada dentro del listado mostrado</param>
        public UnaTesisWin(ObservableCollection<TesisDto> listaTesis, int posActual, Terminos terminoSeleccionado)
        {
            InitializeComponent();
            this.PosActual = posActual;
            this.ListaTesis = listaTesis;
            this.terminoSeleccionado = terminoSeleccionado;
            controller = new UnaTesisWinController(this, listaTesis[posActual]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            RadRibbonButton ribbon = sender as RadRibbonButton;

            switch (ribbon.Name)
            {
                case "RbtnInicio":
                    controller.TesisStart();
                    break;
                case "RbtnPrevious":
                    controller.TesisPrevious();
                    break;
                case "RbtnNext":
                    controller.TesisNext();
                    break;
                case "RbtnFin":
                    controller.TesisEnd();
                    break;
                //case "RbtnClipboard":
                //    controller.TesisToClipboard(1);
                //    break;
                //case "BtnCIus":
                //    controller.TesisToClipboard(2);
                //    break;
                //case "BtnCRubro":
                //    controller.TesisToClipboard(3);
                //    break;
                //case "BtnCTexto":
                //    controller.TesisToClipboard(4);
                //    break;
                //case "BtnCPrec":
                //    controller.TesisToClipboard(5);
                //    break;
            }
        }

        private void RBtnEjecutoria_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportarGroupClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelRelacion_Click(object sender, RoutedEventArgs e)
        {
            controller.NoPertinente();
        }
    }
}
