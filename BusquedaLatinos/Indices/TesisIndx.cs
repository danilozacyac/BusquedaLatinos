using System;
using System.Linq;

namespace BusquedaLatinos.Indices
{
    public class TesisIndx
    {
        private int ius;
        private string rubro;
        private string texto;
        private string rubroIndx;
        private string textoIndx;
        private int volumen;
        private float score;
        
        public int Ius
        {
            get
            {
                return this.ius;
            }
            set
            {
                this.ius = value;
            }
        }

        public string Rubro
        {
            get
            {
                return this.rubro;
            }
            set
            {
                this.rubro = value;
            }
        }

        public string Texto
        {
            get
            {
                return this.texto;
            }
            set
            {
                this.texto = value;
            }
        }

        public string RubroIndx
        {
            get
            {
                return this.rubroIndx;
            }
            set
            {
                this.rubroIndx = value;
            }
        }

        public string TextoIndx
        {
            get
            {
                return this.textoIndx;
            }
            set
            {
                this.textoIndx = value;
            }
        }

        public int Volumen
        {
            get
            {
                return this.volumen;
            }
            set
            {
                this.volumen = value;
            }
        }

        public float Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }
    }
}
