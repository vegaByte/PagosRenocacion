using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class StatusVM : ViewModelBase
    {
        public StatusVM()
        {
            try
            {
                if (statusList == null)
                {
                    statusList = new List<prc_status>(DB.contexto.prc_status.ToList());
                }
            }

            catch { }
        }
        List<PagosRenovacion.prc_status> statusList;

        public List<PagosRenovacion.prc_status> StatusList
        {
            get { return statusList; }
            set { statusList = value; OnPropertyChanged(); }
        }
    }
}
