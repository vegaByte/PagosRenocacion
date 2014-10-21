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
    public class ActividadesVM : ViewModelBase
    {
        public ActividadesVM()
        {
            try
            {
                if (actividadesList == null)
                {
                    actividadesList = new List<PagosRenovacion.prc_actividades>(DB.contexto.prc_actividades.ToList());
                }
            }
            catch { }
        }
            List<PagosRenovacion.prc_actividades> actividadesList;
            public List<PagosRenovacion.prc_actividades> ActividadesList{
                get
                {
                    return actividadesList;
                }
                set
                {
                    actividadesList = value;
                    OnPropertyChanged();
                }
            }
}
}