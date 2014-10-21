using PagosRenovacion.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PagosRenovacion
{
    public class ClassDataGrid
    {
        public ClassDataGrid(WindowAdministrar ventana, int index)
        {
            switch(index){
                case 0:                    
                    ventana.dataGrid1.ItemsSource = DB.contexto.prc_conceptos.ToList();
                    ventana.columna.Binding = new Binding("id_conceptos");
                    break;
                case 1:
                    ventana.dataGrid1.ItemsSource = DB.contexto.prc_tipopagos.ToList();
                    ventana.columna.Binding = new Binding("id_tipopagos");
                    break;
                case 2:
                    ventana.dataGrid1.ItemsSource = DB.contexto.prc_status.ToList();
                    ventana.columna.Binding = new Binding("id_status");
                    break;
                case 3:
                    ventana.dataGrid1.ItemsSource = DB.contexto.prc_actividades.ToList();
                    ventana.columna.Binding = new Binding("id_actividades");
                    break;
            }
        }
    }
}
