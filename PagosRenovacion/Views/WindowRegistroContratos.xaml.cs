using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Collections;

namespace PagosRenovacion.Views
{
    /// <summary>
    /// Lógica de interacción para WindowRegistroContratos.xaml
    /// </summary>
    public partial class WindowRegistroContratos : Window
    {
        Validator validador;
        public WindowRegistroContratos()
        {
            validador = new Validator();
            DateTime hoy = DateTime.Today;

            InitializeComponent();
            dataGrid1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0FFFF"));
            dateFin.SelectedDate = hoy.AddYears(1);
            dateInicio.SelectedDate = new DateTime(hoy.Year - 1, hoy.Month, hoy.Day); 
        }
        List<prc_view_date_contratos> miResultado;
        IList miResultadoReport;
        private List<prc_date_contratos> busquedaAvanzada()
        {
            //concepto(nombre) fecha(dia, mes, año) status(nombre)
            DateTime busquedaFecha;
            int busquedaNum;

            DateTime.TryParse(busquedaTextbox.Text, out busquedaFecha);
            int.TryParse(busquedaTextbox.Text, out busquedaNum);
            try
            {
                List<prc_date_contratos> resultadoBusqueda;

                if (dateInicio.SelectedDate == null || dateFin.SelectedDate == null)
                {
                    resultadoBusqueda = DB.contexto.prc_date_contratos.Where
                    (a => a.prc_contratos.concepto.Contains(busquedaTextbox.Text) ||
                     a.prc_contratos.prc_actividades.nombre.Contains(busquedaTextbox.Text) ||
                     a.fecha_nota.Day.Equals(busquedaNum) ||
                     a.fecha_nota.Year.Equals(busquedaNum)).ToList().
                     Union(DB.contexto.prc_date_contratos.AsEnumerable().Where(
                     a => a.fecha_nota.ToString("MMMM").Contains(busquedaTextbox.Text))).ToList();
                }
                else
                {
                    resultadoBusqueda = DB.contexto.prc_date_contratos.Where
                    (a => (a.fecha_nota >= dateInicio.SelectedDate && a.fecha_nota <= dateFin.SelectedDate) &&
                     (a.prc_contratos.concepto.Contains(busquedaTextbox.Text) ||
                     a.prc_contratos.prc_actividades.nombre.Contains(busquedaTextbox.Text) ||
                     a.fecha_nota.Day.Equals(busquedaNum) ||
                     a.fecha_nota.Year.Equals(busquedaNum))).ToList().
                     Union(DB.contexto.prc_date_contratos.AsEnumerable().Where(
                     a => (a.fecha_nota >= dateInicio.SelectedDate && a.fecha_nota <= dateFin.SelectedDate) &&
                         (a.fecha_nota.ToString("MMMM").Contains(busquedaTextbox.Text)))).ToList();
                }
                miResultado = DB.contexto.prc_view_date_contratos.ToList();

                var query = (from view in miResultado
                             join find in resultadoBusqueda on view.concepto+view.fecha_nota+view.nombre equals find.prc_contratos.concepto+find.fecha_nota+find.prc_contratos.prc_actividades.nombre
                             select view).ToList();

                miResultadoReport = (query as IList);

                return resultadoBusqueda;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexión.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void busquedaTextbox_KeyUp(object sender, KeyEventArgs e)
        {
             dataGrid1.ItemsSource = busquedaAvanzada();
        }

        private void dateInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateInicio.DisplayDateEnd = dateFin.SelectedDate;
            dateFin.DisplayDateStart = dateInicio.SelectedDate;
            if (dateInicio.SelectedDate != null && dateFin.SelectedDate != null)
            {
                dataGrid1.ItemsSource = busquedaAvanzada();
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            WindowAltaContrato vtnNuevoContrato = new WindowAltaContrato();
            vtnNuevoContrato.ShowDialog();
            dataGrid1.ItemsSource = busquedaAvanzada();
        }

        private void chbxEdicion_Checked(object sender, RoutedEventArgs e)
        {
            dataGrid1.IsReadOnly = false;
            MessageBox.Show("Edición habilitada.", "Editar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void chbxEdicion_Unchecked(object sender, RoutedEventArgs e)
        {
            dataGrid1.IsReadOnly = true;
        }

        private bool isManualEditCommit;
        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (chbxEdicion.IsChecked == true)
            {
                if (!isManualEditCommit)
                {
                    isManualEditCommit = true;
                    DataGrid grid = (DataGrid)sender;

                    var vtnEmergente = MessageBox.Show("Realmente desea editar el registro?", "Cambio en registro", MessageBoxButton.YesNo);
                    if (vtnEmergente == MessageBoxResult.Yes)
                    {
                        grid.CommitEdit(DataGridEditingUnit.Row, true);
                        DB.contexto.SaveChanges();
                        dataGrid1.ItemsSource = busquedaAvanzada();
                    }
                    else
                    {
                        grid.CancelEdit();
                    }
                    isManualEditCommit = false;
                }
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FormReporterContratos form = new FormReporterContratos(miResultadoReport);
            form.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            busquedaTextbox.Focus();
        }

    }
}
