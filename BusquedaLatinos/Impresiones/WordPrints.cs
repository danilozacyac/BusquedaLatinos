using BusquedaLatinos.Dto;
using BusquedaLatinos.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using ScjnUtilities;
namespace BusquedaLatinos.Impresiones
{
    public class WordPrints
    {

        Word.Application oWord;
        Word.Document oDoc;
        object oMissing = System.Reflection.Missing.Value;
        List<string> letrasImprimir;


        public WordPrints(List<string> letrasImprimir)
        {
            this.letrasImprimir = letrasImprimir;
        }


        public WordPrints() { }

        public void GeneraDocumentoWord()
        {
            
            oWord = new Microsoft.Office.Interop.Word.Application();
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);


           

            foreach (string letra in letrasImprimir)
            {
                Microsoft.Office.Interop.Word.Paragraph titulo = oDoc.Content.Paragraphs.Add(ref oMissing);
                titulo.Range.Font.Bold = 1;
                titulo.Range.Font.Size = 72;
                titulo.Range.Font.Name = "Algerian";
                titulo.Range.Text = letra;
                titulo.Range.InsertParagraphAfter();

                ObservableCollection<Terminos> voces = (from n in TerminosSingleton.Terminos
                                                        where n.TerminoStr.StartsWith(letra)
                                                        orderby n.TerminoStr
                                                        select n).ToObservableCollection();

                this.PrintTerminos(voces);

                oDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
            }
            oWord.Visible = true;
        }


        /// <summary>
        /// Permite imprimir el terminos seleccionado 
        /// </summary>
        /// <param name="imprimir"></param>
        public void ImprimeSelección(Terminos termino)
        {
            oWord = new Microsoft.Office.Interop.Word.Application();
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            this.PrintTerminos(new ObservableCollection<Terminos>() { termino });

            oWord.Visible = true;
        }

        private void PrintTerminos(ObservableCollection<Terminos> terminosPrint)
        {
            foreach (Terminos termino in terminosPrint)
            {
                if (!String.IsNullOrEmpty(termino.Definicion))
                {
                    Microsoft.Office.Interop.Word.Paragraph par = oDoc.Content.Paragraphs.Add(ref oMissing);
                    par.Range.Font.Bold = 1;
                    par.Range.Font.Size = 14;
                    par.Range.Font.Name = "Arial";

                    par.Range.Text = termino.Termino;
                    par.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    par.Range.InsertParagraphAfter();

                    par.Range.Text = termino.Definicion;
                    par.Range.Font.Bold = 0;
                    par.Range.Font.Size = 12;
                    par.Range.InsertParagraphAfter();
                    par.Range.InsertParagraphAfter();

                    par.Range.Font.Bold = 0;


                    par.Range.InsertParagraphAfter();
                }
            }
        }

    }
}
