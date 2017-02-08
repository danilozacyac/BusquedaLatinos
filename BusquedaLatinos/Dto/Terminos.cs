using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MantesisApi.Dto;

namespace BusquedaLatinos.Dto
{
    public class Terminos
    {
        private int idTermino;
        private string termino;
        private string terminoStr;
        private string definicion;
        private string bibliografia;
        private List<int> iuses;
        private ObservableCollection<TesisDto> tesis;
        
        public int IdTermino
        {
            get
            {
                return this.idTermino;
            }
            set
            {
                this.idTermino = value;
            }
        }

        public string Termino
        {
            get
            {
                return this.termino;
            }
            set
            {
                this.termino = value;
            }
        }

        public string TerminoStr
        {
            get
            {
                return this.terminoStr;
            }
            set
            {
                this.terminoStr = value;
            }
        }

        public string Definicion
        {
            get
            {
                return this.definicion;
            }
            set
            {
                this.definicion = value;
            }
        }

        public string Bibliografia
        {
            get
            {
                return this.bibliografia;
            }
            set
            {
                this.bibliografia = value;
            }
        }

        public List<int> Iuses
        {
            get
            {
                return this.iuses;
            }
            set
            {
                this.iuses = value;
            }
        }

        public ObservableCollection<TesisDto> Tesis
        {
            get
            {
                return this.tesis;
            }
            set
            {
                this.tesis = value;
            }
        }
    }
}
