using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    class ViewDatePagosVM : ViewModelBase
    {
        public ViewDatePagosVM()
        {
            try
            {
                if (viewDatePagoList == null)
                {
                    viewDatePagoList = new List<prc_view_date_pagos>(DB.contexto.prc_view_date_pagos.ToList());
                }
            }
            catch { }
        }
        List<prc_view_date_pagos> viewDatePagoList;

        public List<prc_view_date_pagos> ViewDatePagoList
        {
            get { return viewDatePagoList; }
            set { viewDatePagoList = value; OnPropertyChanged(); }
        }
    }
}
