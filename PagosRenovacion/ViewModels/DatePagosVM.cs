using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class DatePagosVM : ViewModelBase
    {
        public DatePagosVM()
        {
            try
            {
                if (datePagosList == null)
                {
                    datePagosList = new List<PagosRenovacion.prc_date_pagos>(DB.contexto.prc_date_pagos.ToList());
                }
            }
            catch { }
        }
        List<PagosRenovacion.prc_date_pagos> datePagosList;

        public List<PagosRenovacion.prc_date_pagos> DatePagosList
        {
            get { return datePagosList; }
            set { datePagosList = value; OnPropertyChanged(); }
        }

    }
}
