using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class DateContratosVM : ViewModelBase
    {
        public DateContratosVM()
        {
            try
            {
                if (dateContratosList == null)
                {
                    dateContratosList = new List<PagosRenovacion.prc_date_contratos>(DB.contexto.prc_date_contratos.ToList());
                }
            }

            catch { }
        }
        List<PagosRenovacion.prc_date_contratos> dateContratosList;
        public List<PagosRenovacion.prc_date_contratos> DateContratosList
        {
            get
            {
                return dateContratosList;
            }
            set
            {
                dateContratosList = value;
                OnPropertyChanged();
            }
        }
    }
}
