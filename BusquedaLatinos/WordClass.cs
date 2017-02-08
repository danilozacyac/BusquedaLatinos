using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusquedaLatinos.Dto;
using Microsoft.Office.Interop.Word;
using ScjnUtilities;
using System.Collections.ObjectModel;

namespace BusquedaLatinos
{
    public class WordClass
    {

        ObservableCollection<Terminos> terminos;
        private readonly string tituloObra;
        private string presentacion;

        private int fila = 1;
        int folio = 1;

        string imagenAutorizada = String.Empty;

        private int oficioInicial = 0;
        private int contadorOficio = 0;

        /// <summary>
        /// Indica si los acuses de recibo de una obra en particular se están generando por primera vez 
        /// o no. De esta forma podemos llevar el control de que oficios se fueron para cada persona
        /// </summary>
        private readonly bool vuelveAGenerarOficio = false;


        Microsoft.Office.Interop.Word.Application oWord;
        Microsoft.Office.Interop.Word.Document aDoc;
        object oMissing = System.Reflection.Missing.Value;
        object oEndOfDoc = "\\endofdoc";


        //readonly string aclaraciones = ConfigurationManager.AppSettings["Aclaraciones"].ToString();
        //readonly string titularCoord = ConfigurationManager.AppSettings["TitularCoord"].ToString();

        readonly string filePath;


        public WordClass(ObservableCollection<Terminos> terminos, string filePath)
        {
            this.terminos = terminos;
            this.filePath = filePath;
        }






        #region Organismos


        public void ImprimeResultados()
        {

            string nuevoDoc = Path.GetTempFileName() + ".docx";

            try
            {

                oWord = new Microsoft.Office.Interop.Word.Application();
                aDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);



                Paragraph oPara1;
                oPara1 = aDoc.Content.Paragraphs.Add(ref oMissing);
                //oPara1.Range.ParagraphFormat.Space1;
                oPara1.Range.Text = "Reporte de términos latinos";
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                oPara1.Range.Font.Size = 16;
                oPara1.Range.Font.Name = "Arial";
                oPara1.Format.SpaceAfter = 0;    //24 pt spacing after paragraph.
                oPara1.Range.InsertParagraphAfter();


                foreach (Terminos terminoPrint in terminos)
                {

                   
                    this.ParagraphAfter(oPara1, 2);

                    Range wrdRng = aDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    oPara1.Range.Text = String.Format("{0} ({1} registros)",terminoPrint.Termino, terminoPrint.Iuses.Count);
                    oPara1.Range.Font.Bold = 1;
                    oPara1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    oPara1.Range.Font.Size = 14;

                    oPara1.Range.InsertParagraphAfter();

                    oPara1.Range.Text = string.Join(", ", terminoPrint.Iuses);
                    oPara1.Range.Font.Bold = 0;
                    oPara1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    oPara1.Range.Font.Size = 10;

                    

                }
                
                aDoc.SaveAs(nuevoDoc);
                oWord.Visible = true;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,WordReports", "PadronApi");
            }
        }


        #endregion



     

        /// <summary>
        /// Inserta saltos de línea en un documento
        /// </summary>
        /// <param name="oPara"></param>
        /// <param name="cuantos">Cuantos saltos de línea consecutivos agregará</param>
        private void ParagraphAfter(Paragraph oPara, int cuantos)
        {
            while (cuantos > 0)
            {
                oPara.Range.InsertParagraphAfter();
                cuantos--;
            }
        }

       






        



    
    }
}
