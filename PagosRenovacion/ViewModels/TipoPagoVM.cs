using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class TipoPagoVM : ViewModelBase
    {
        public TipoPagoVM()
        {
            try
            {
                if (tipoPagoList == null)
                {
                    tipoPagoList = new List<prc_tipopagos>(DB.contexto.prc_tipopagos.ToList());
                }
            }

            catch { }
        }
        List<PagosRenovacion.prc_tipopagos> tipoPagoList;

        public List<PagosRenovacion.prc_tipopagos> TipoPagoList
        {
            get { return tipoPagoList; }
            set { tipoPagoList = value; OnPropertyChanged(); }
        }
    }
}
