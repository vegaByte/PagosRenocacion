using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class ContratosVM : ViewModelBase
    {
        public ContratosVM()
        {
            try
            {
                if (contratosList == null)
                {
                    contratosList = new List<prc_contratos>(DB.contexto.prc_contratos.ToList());
                }
            }

            catch { }
        }
        List<PagosRenovacion.prc_contratos> contratosList;
        public List<PagosRenovacion.prc_contratos> ContratosList
        {
            get { return contratosList; }
            set
            {
                contratosList = value;
                OnPropertyChanged();
            }
        }
    }
}
