using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace BusquedaLatinos.Indices
{
    public class MyLucene
    {
        private Analyzer analyzer = new StandardAnalyzer(new string[]{"desde"});
        private Directory luceneIndexDirectory;
        private IndexWriter writer;
        private readonly string indexPath = ConfigurationManager.AppSettings["IndexPath"];

        List<TesisIndx> listaTesis;

        public MyLucene(List<TesisIndx> listaTesis)
        {
            this.listaTesis = listaTesis;
        }

        public MyLucene()
        {

        }

       /// <summary>
       /// Acumula el índice de las tesis que mes a mes se van ingresando a través del 
       /// sistema de mantenimiento
       /// </summary>
        public void BuildIndex()
        {
            try
            {
                //if (System.IO.Directory.Exists(indexPath))
                //{
                //    System.IO.Directory.Delete(indexPath, true);
                //}

                luceneIndexDirectory = FSDirectory.GetDirectory(indexPath);
                writer = new IndexWriter(luceneIndexDirectory, analyzer, false);

                int consecTesis = 1;
                foreach (TesisIndx tesis in listaTesis)
                {
                    Document doc = new Document();
                    doc.Add(new Field("Ius", tesis.Ius.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));
                    doc.Add(new Field("RubroIndx", tesis.RubroIndx, Field.Store.YES, Field.Index.TOKENIZED));
                    doc.Add(new Field("TextoIndx", tesis.TextoIndx, Field.Store.YES, Field.Index.TOKENIZED));
                    doc.Add(new Field("Rubro", tesis.Rubro, Field.Store.YES, Field.Index.UN_TOKENIZED));

                    writer.AddDocument(doc);
                    Console.WriteLine(consecTesis);
                    consecTesis++;
                }
                writer.Optimize();
                writer.Flush();
                writer.Close();
                luceneIndexDirectory.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        

        public List<TesisIndx> Search(string searchTerm)
        {
            List<int> listaIuses = new List<int>();
            List<TesisIndx> results = new List<TesisIndx>();

            IndexSearcher searcher = new IndexSearcher(FSDirectory.GetDirectory(indexPath));
            QueryParser parser = new QueryParser("RubroIndx", analyzer);
            parser.SetEnablePositionIncrements(false);


            Query query = parser.Parse(String.Format("\"{0}\"", searchTerm));

            Console.WriteLine(query.ToString());
            Hits hitsFound = searcher.Search(query);

            TesisIndx tesis = null;

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                tesis = new TesisIndx();
                Document doc = hitsFound.Doc(i);
                tesis.Ius = int.Parse(doc.Get("Ius"));
                tesis.RubroIndx = doc.Get("RubroIndx");
                tesis.TextoIndx = doc.Get("TextoIndx");
                tesis.Rubro = doc.Get("Rubro");
                listaIuses.Add(tesis.Ius);
                //float score = hitsFound.Score(i);
                //tesis.Score = score;

                results.Add(tesis);
            }



            parser = new QueryParser("TextoIndx", analyzer);
            parser.SetEnablePositionIncrements(false);

            query = parser.Parse(String.Format("\"{0}\"", searchTerm));
            Console.WriteLine(query.ToString());
            hitsFound = searcher.Search(query);

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                tesis = new TesisIndx();
                Document doc = hitsFound.Doc(i);
                tesis.Ius = int.Parse(doc.Get("Ius"));
                tesis.Rubro = doc.Get("RubroIndx");
                tesis.Texto = doc.Get("TextoIndx");
                tesis.Rubro = doc.Get("Rubro");
                //float score = hitsFound.Score(i);
                //tesis.Score = score;
                if(!listaIuses.Contains(tesis.Ius))
                    results.Add(tesis);
            }


            return (from n in results orderby n.Ius select n).ToList();
        }

        public IEnumerable<TesisIndx> BoolSearch(string searchTerm)
        {

            //BooleanQuery booleanQ = new BooleanQuery();
            //booleanQ.Add()

            List<TesisIndx> results = new List<TesisIndx>();

            IndexSearcher searcher = new IndexSearcher(FSDirectory.GetDirectory(indexPath));
            QueryParser parser = new QueryParser("RubroIndx", analyzer);
            parser.SetEnablePositionIncrements(false);


            Query query = parser.Parse(String.Format("\"{0}\"", searchTerm));

            Console.WriteLine(query.ToString());
            Hits hitsFound = searcher.Search(query);

            TesisIndx tesis = null;

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                tesis = new TesisIndx();
                Document doc = hitsFound.Doc(i);
                tesis.Ius = int.Parse(doc.Get("Ius"));
                tesis.RubroIndx = doc.Get("RubroIndx");
                tesis.TextoIndx = doc.Get("TextoIndx");
                tesis.Rubro = doc.Get("Rubro");
                //float score = hitsFound.Score(i);
                //tesis.Score = score;

                results.Add(tesis);
            }



            parser = new QueryParser("TextoIndx", analyzer);
            parser.SetEnablePositionIncrements(false);

            query = parser.Parse(String.Format("\"{0}\"", searchTerm));
            Console.WriteLine(query.ToString());
            hitsFound = searcher.Search(query);

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                tesis = new TesisIndx();
                Document doc = hitsFound.Doc(i);
                tesis.Ius = int.Parse(doc.Get("Ius"));
                tesis.RubroIndx = doc.Get("RubroIndx");
                tesis.TextoIndx = doc.Get("TextoIndx");
                tesis.Rubro = doc.Get("Rubro");
                //float score = hitsFound.Score(i);
                //tesis.Score = score;

                results.Add(tesis);
            }

            results = results.Distinct().ToList();

            return results;
        }
    

        //public List<int> SearchIuses(string searchTerm)
        //{
        //    List<int> results = new List<int>();

        //    IndexSearcher searcher = new IndexSearcher(FSDirectory.GetDirectory(indexPath));
        //    QueryParser parser = new QueryParser("Rubro", analyzer);
        //    parser.SetEnablePositionIncrements(false);

        //    PhraseQuery q = new PhraseQuery();
        //    String[] words = searchTerm.Split(' ');

        //    foreach (string word in words)
        //    {
        //        q.Add(new Term("Rubro", word));
        //    }
        //    Console.WriteLine(q.ToString());
        //    //Query query = parser.Parse(searchTerm);
        //    Hits hitsFound = searcher.Search(q);

        //    TesisIndx sampleDataFileRow = null;

        //    for (int i = 0; i < hitsFound.Length(); i++)
        //    {
        //        sampleDataFileRow = new TesisIndx();
        //        Document doc = hitsFound.Doc(i);
        //        sampleDataFileRow.Ius = int.Parse(doc.Get("Ius"));
        //        sampleDataFileRow.Rubro = doc.Get("Rubro");
        //        sampleDataFileRow.Texto = doc.Get("Texto");
        //        float score = hitsFound.Score(i);
        //        sampleDataFileRow.Score = score;

        //        results.Add(sampleDataFileRow.Ius);
        //    }



        //    parser = new QueryParser("Texto", analyzer);
        //    parser.SetEnablePositionIncrements(false);

        //    q = new PhraseQuery();
        //    words = searchTerm.Split(' ');

        //    foreach (string word in words)
        //    {
        //        q.Add(new Term("Texto", word));
        //    }

        //    // query = parser.Parse(searchTerm);
        //    hitsFound = searcher.Search(q);

        //    for (int i = 0; i < hitsFound.Length(); i++)
        //    {
        //        sampleDataFileRow = new TesisIndx();
        //        Document doc = hitsFound.Doc(i);
        //        sampleDataFileRow.Ius = int.Parse(doc.Get("Ius"));
        //        sampleDataFileRow.Rubro = doc.Get("Rubro");
        //        sampleDataFileRow.Texto = doc.Get("Texto");
        //        float score = hitsFound.Score(i);
        //        sampleDataFileRow.Score = score;

        //        results.Add(sampleDataFileRow.Ius);
        //    }

        //    results.Distinct();

        //    return results;
        //}


        /// <summary>
        /// Busca en el índice previamente construido las tesis que tengan coincidencia ya sea en el Rubro o Texto
        /// del término buscado
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public List<int> SearchIuses(string searchTerm)
        {
            List<int> results = new List<int>();

            IndexSearcher searcher = new IndexSearcher(FSDirectory.GetDirectory(indexPath));
            QueryParser parser = new QueryParser("RubroIndx", analyzer);
            parser.SetEnablePositionIncrements(false);


            Query query = parser.Parse(String.Format("\"{0}\"", searchTerm));

           Console.WriteLine(query.ToString());
            Hits hitsFound = searcher.Search(query);

            TesisIndx sampleDataFileRow = null;

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                sampleDataFileRow = new TesisIndx();
                Document doc = hitsFound.Doc(i);
                sampleDataFileRow.Ius = int.Parse(doc.Get("Ius"));
                sampleDataFileRow.RubroIndx = doc.Get("RubroIndx");
                sampleDataFileRow.TextoIndx = doc.Get("TextoIndx");
                float score = hitsFound.Score(i);
                sampleDataFileRow.Score = score;

                results.Add(sampleDataFileRow.Ius);
            }



            parser = new QueryParser("TextoIndx", analyzer);
            parser.SetEnablePositionIncrements(false);

            query = parser.Parse(String.Format("\"{0}\"", searchTerm));
            Console.WriteLine(query.ToString());
            hitsFound = searcher.Search(query);

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                sampleDataFileRow = new TesisIndx();
                Document doc = hitsFound.Doc(i);
                sampleDataFileRow.Ius = int.Parse(doc.Get("Ius"));
                sampleDataFileRow.Rubro = doc.Get("RubroIndx");
                sampleDataFileRow.Texto = doc.Get("TextoIndx");
                float score = hitsFound.Score(i);
                sampleDataFileRow.Score = score;

                results.Add(sampleDataFileRow.Ius);
            }

            results.Distinct();

            return results;
        }
    }
}