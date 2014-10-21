using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PagosRenovacion.ViewModels
{
    public class DatePagosSemanalesVM : ViewModelBase
    {
        public DatePagosSemanalesVM()
        {
            try
            {
                if (datePagosSemanalesList == null)
                {
                    datePagosSemanalesList = new List<prc_date_pagos_semanales>(DB.contexto.prc_date_pagos_semanales.ToList());
                }
            }

            catch { }
        }
        List<PagosRenovacion.prc_date_pagos_semanales> datePagosSemanalesList;

        public List<PagosRenovacion.prc_date_pagos_semanales> DatePagosSemanalesList
        {
            get { return datePagosSemanalesList; }
            set { datePagosSemanalesList = value; OnPropertyChanged(); }
        }
    }
}
