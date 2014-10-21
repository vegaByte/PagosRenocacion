using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class PagosVM : ViewModelBase
    {
        public PagosVM()
        {
            try
            {
                if (pagosList == null)
                {
                    pagosList = new List<prc_pagos>(DB.contexto.prc_pagos.ToList());
                }
            }
            catch { }
        }
        List<PagosRenovacion.prc_pagos> pagosList;

        public List<PagosRenovacion.prc_pagos> PagosList
        {
            get { return pagosList; }
            set { pagosList = value; OnPropertyChanged(); }
        }
    }
}
