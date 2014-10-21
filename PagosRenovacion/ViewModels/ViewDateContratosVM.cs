using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class ViewDateContratosVM : ViewModelBase
    {
        public ViewDateContratosVM()
        {
            try
            {
                if (viewDateContratosList == null)
                    viewDateContratosList = new List<prc_view_date_contratos>(DB.contexto.prc_view_date_contratos.ToList());
            }
            catch { }
        }
        List<prc_view_date_contratos> viewDateContratosList;

        public List<prc_view_date_contratos> ViewDateContratosList
        {
            get { return viewDateContratosList; }
            set { viewDateContratosList = value; OnPropertyChanged(); }
        }

        
    }
}
