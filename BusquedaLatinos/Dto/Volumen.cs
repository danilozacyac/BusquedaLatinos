using System;
using System.Linq;

namespace BusquedaLatinos.Dto
{
    public class Volumen
    {
        private int volumen;
        private string descripcion;

        public int Volumen1
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

        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
            }
        }
    }
}
