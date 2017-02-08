using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using BusquedaLatinos.Indices;
using MantesisApi.Converters;
using MantesisApi.Dto;
using MantesisApi.Model;
using BusquedaLatinos.Model;

namespace BusquedaLatinos.Controllers
{
   public  class UnaTesisWinController
    {
       private readonly UnaTesisWin unaTesis;


        private TesisDto tesisIndice = null;

        TesisDto tesisMostrada;

        public UnaTesisWinController(UnaTesisWin unaTesis, TesisDto tesisIndice)
        {
            this.unaTesis = unaTesis;
            this.tesisIndice = tesisIndice;
            LoadCompleteTesis(tesisIndice);
        }


        private void LoadCompleteTesis(TesisDto tesisBuscar)
        {
            unaTesis.flowDoc.Blocks.Clear();
            tesisMostrada = new TesisDtoModel().GetTesis(tesisBuscar.Ius);
            
            LoadTesis();
            HighlihtText(unaTesis.terminoSeleccionado.Termino.ToLower());
            HighlihtText(unaTesis.terminoSeleccionado.Termino.ToUpper());
            HighlihtText(unaTesis.terminoSeleccionado.Termino);

           string prueba = new MateriaConverter().Convert(tesisMostrada.Materia1, null, null, null).ToString();

            unaTesis.DataContext = tesisMostrada;
        }


        /// <summary>
        /// Carga la información de la tesis en cada uno de los campos del formulario
        /// </summary>
        public void LoadTesis()
        {
            

            //tesisIndice.IsReadOnly = true;

            //unaTesis.DataContext = tesisIndice;

            if (unaTesis.ListaTesis != null && unaTesis.ListaTesis.Count > 1)
                unaTesis.LblContador.Content = String.Format("     {0} / {1}", (unaTesis.PosActual + 1), unaTesis.ListaTesis.Count);
            else
            {
                unaTesis.LblContador.Content = "    1 / 1";
                unaTesis.Navega.IsEnabled = false;
            }

            //Agregamos el rubro
            this.LoadParteTesis(tesisMostrada.Rubro, 1);

            //Agregamos el texto
            this.LoadParteTesis(tesisMostrada.Texto, 2);

            //Agregamos precedentes
            this.LoadParteTesis(tesisMostrada.Precedentes, 4);

            //Agregamos tribunal
            //this.LoadParteTesis(tesisMostrada, ligasTesis, 4);

            //Agregamos Nota Publica
            this.LoadParteTesis(tesisMostrada.NotaPublica, 1000);

            LoadNoBindingValues();

           // unaTesis.TxtSemanario.Text = "Semanario Judicial de la Federación, " + this.GetTextoPublicaSemanario(tesisMostrada.Precedentes);
            //unaTesis.TxtGaceta.Text = this.GetTextoPublicaGaceta();
        }

        public void LoadNoBindingValues()
        {
            unaTesis.RbtAislada.FontWeight = FontWeights.Normal;
            unaTesis.RbtJurisp.FontWeight = FontWeights.Normal;

            if (tesisMostrada.TaTj == 0)
            {
                unaTesis.RbtAislada.IsChecked = true;
                unaTesis.RbtAislada.FontWeight = FontWeights.Bold;
            }
            else
            {
                unaTesis.RbtJurisp.IsChecked = true;
                unaTesis.RbtJurisp.FontWeight = FontWeights.Bold;
            }

        }


        public void LoadParteTesis(string texto, int seccion)
        {
            Paragraph paragraphTesis = new Paragraph();
            paragraphTesis.FontSize = 12;
            paragraphTesis.FontWeight = FontWeights.Normal;
            paragraphTesis.Inlines.Add(new Run(texto));
            unaTesis.flowDoc.Blocks.Add(paragraphTesis);
        }


        private void HighlihtText(string searchTerm)
        {
            for (TextPointer position = unaTesis.flowDoc.ContentStart;
        position != null && position.CompareTo(unaTesis.flowDoc.ContentEnd) <= 0;
        position = position.GetNextContextPosition(LogicalDirection.Forward))
            {
                //if (position.CompareTo(unaTesis.flowDoc.ContentEnd) == 0)
                //{
                //    return unaTesis.flowDoc;
                //}

                String textRun = position.GetTextInRun(LogicalDirection.Forward);
                StringComparison stringComparison = StringComparison.CurrentCulture;
                Int32 indexInRun = textRun.IndexOf(searchTerm, stringComparison);
                if (indexInRun >= 0)
                {
                    position = position.GetPositionAtOffset(indexInRun);
                    if (position != null)
                    {
                        TextPointer nextPointer = position.GetPositionAtOffset(searchTerm.Length);
                        TextRange textRange = new TextRange(position, nextPointer);
                        textRange.ApplyPropertyValue(TextElement.BackgroundProperty,
                                      new SolidColorBrush(Colors.Yellow));
                    }
                }
            }
        }



        public void FlowdocBackgroundColor()
        {
            if (unaTesis.ColorFondo == 0)
                unaTesis.flowDoc.Background = new SolidColorBrush(Colors.White);
            if (unaTesis.ColorFondo == 1)
                unaTesis.flowDoc.Background = new SolidColorBrush(Colors.LightPink);
            if (unaTesis.ColorFondo == 2)
                unaTesis.flowDoc.Background = new SolidColorBrush(Colors.LightGreen);
        }

        #region RibbonButtons

        public void TesisStart()
        {
            tesisIndice = null;
            unaTesis.PosActual = 0;
            TesisDto tesis = unaTesis.ListaTesis[unaTesis.PosActual];
            this.LoadCompleteTesis(tesis);

            unaTesis.LblContador.Content = String.Format("     {0} / {1}", (unaTesis.PosActual + 1), unaTesis.ListaTesis.Count);
        }

        public void TesisPrevious()
        {
            tesisIndice = null;
            if (unaTesis.PosActual > 0)
            {
                unaTesis.PosActual--;
                TesisDto tesis = unaTesis.ListaTesis[unaTesis.PosActual];
                this.LoadCompleteTesis(tesis);

                unaTesis.LblContador.Content = String.Format("     {0} / {1}", (unaTesis.PosActual + 1), unaTesis.ListaTesis.Count);
            }
        }

        public void TesisNext()
        {
            tesisIndice = null;
            if (unaTesis.PosActual < unaTesis.ListaTesis.Count - 1)
            {
                unaTesis.PosActual++;
                TesisDto tesis = unaTesis.ListaTesis[unaTesis.PosActual];

                //unaTesisModel.DbConnectionOpen();
                this.LoadCompleteTesis(tesis);
                //unaTesisModel.DbConnectionClose();

                unaTesis.LblContador.Content = String.Format("     {0} / {1}", (unaTesis.PosActual + 1), unaTesis.ListaTesis.Count);
            }
        }

        public void TesisEnd()
        {
            tesisIndice = null;

            unaTesis.PosActual = unaTesis.ListaTesis.Count - 1;
            TesisDto tesis = unaTesis.ListaTesis[unaTesis.PosActual];

            //unaTesisModel.DbConnectionOpen();
            this.LoadCompleteTesis(tesis);
            //unaTesisModel.DbConnectionClose();
            unaTesis.LblContador.Content = String.Format("     {0} / {1}", (unaTesis.PosActual + 1), unaTesis.ListaTesis.Count);
        }




        private string GetTextoPublicaSemanario(string precedentes)
        {
            string[] cadenas = precedentes.Replace("\n", "").Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            string publica = cadenas[cadenas.Count() - 1];

            int posicion = publica.IndexOf("horas");

            if(posicion != -1)
            return publica.Substring(24, posicion - 24) + " horas";
            else
            return String.Empty;
        }


        public void NoPertinente()
        {
            new TerminosModel().UpdatePertinencia(unaTesis.terminoSeleccionado, tesisMostrada.Ius, false);

            TesisDto tesisDel = (from n in unaTesis.terminoSeleccionado.Tesis
                                 where n.Ius == tesisMostrada.Ius
                                 select n).ToList()[0] as TesisDto;

            unaTesis.terminoSeleccionado.Tesis.Remove(tesisDel);


        }

       

        #endregion

        //#region Exportar

        //public void ExportarOptions(string name)
        //{
        //    //switch (name)
        //    //{
        //    //    case "RBtnPdf":
        //    //        new ListaTesisPdf().GeneraPdfConDetalleTesis(tesisMostrada);
        //    //        break;
        //    //    case "RBtnWord":

        //    //        new ListaTesisWord().GeneraWordConDetalleTesis(tesisMostrada);
        //    //        break;
        //    //}
        //}

        //#endregion


    }
}
