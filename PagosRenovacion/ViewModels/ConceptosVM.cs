using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PagosRenovacion.ViewModels
{
    public class ConceptosVM : ViewModelBase
    {
        public ConceptosVM()
        {
            try
            {
                if (conceptosList == null)
                {
                    conceptosList = new List<PagosRenovacion.prc_conceptos>(DB.contexto.prc_conceptos.ToList());
                }
            }

            catch { }
        }
        List<PagosRenovacion.prc_conceptos> conceptosList;
        public List<PagosRenovacion.prc_conceptos> ConceptosList
        {
            get
            {
                return conceptosList;
            }
            set
            {
                conceptosList = value;
                OnPropertyChanged();
            }
        }
    }
}
