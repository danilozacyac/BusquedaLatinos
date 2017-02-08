using BusquedaLatinos.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScjnUtilities;
using BusquedaLatinos.Model;

namespace BusquedaLatinos.Singleton
{
    public class TerminosSingleton
    {
        private static ObservableCollection<Terminos> termimnos;


        public static ObservableCollection<Terminos> Terminos
        {
            get
            {
                if (termimnos == null)
                    termimnos = new TerminosModel().GetTerminos();

                return termimnos;
            }
        }


    }
}
