using System;
using System.Collections.Generic;
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
using PagosRenovacion.Views;
using System.Collections;

namespace PagosRenovacion
{
    /// <summary>
    /// Lógica de interacción para WindowReportePagos.xaml
    /// </summary>
    public partial class WindowReportePagos : Window
    {
        Validator validador;
        public WindowReportePagos()
        {
            DateTime hoy = DateTime.Today;
            InitializeComponent();
            gridPagosProgramados.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0FFFF"));
            dateFin.SelectedDate = hoy.AddYears(1);
            dateInicio.SelectedDate = new DateTime(hoy.Year - 1, hoy.Month, hoy.Day);
            validador = new Validator();
        }
        List<prc_view_date_pagos> miResultado;
        IList miResultadoReport;
        private List<prc_date_pagos> busquedaAvanzada()
        {
            //concepto(nombre) fecha(dia, mes, año) status(nombre)
            DateTime busquedaFecha;
            int busquedaNum;

            DateTime.TryParse(busquedaTextbox.Text, out busquedaFecha);
            int.TryParse(busquedaTextbox.Text, out busquedaNum);
            try
            {
                List<prc_date_pagos> resultadoConsulta;

                if (dateInicio.SelectedDate == null || dateFin.SelectedDate == null)
                {
                    resultadoConsulta = DB.contexto.prc_date_pagos.Where
                    (a =>
                     a.prc_status.nombre.Contains(busquedaTextbox.Text) ||
                     a.prc_pagos.prc_conceptos.nombre.Contains(busquedaTextbox.Text) ||
                     a.fecha_nota.Day.Equals(busquedaNum) ||
                     a.fecha_nota.Year.Equals(busquedaNum)).ToList().
                     Union(DB.contexto.prc_date_pagos.AsEnumerable().Where(
                     a => a.fecha_nota.ToString("MMMM").Contains(busquedaTextbox.Text))).ToList();
                }
                else
                {
                    resultadoConsulta = DB.contexto.prc_date_pagos.Where
                    (a =>
                     (a.fecha_nota >= dateInicio.SelectedDate && a.fecha_nota <= dateFin.SelectedDate) &&
                     (a.prc_status.nombre.Contains(busquedaTextbox.Text) ||
                     a.prc_pagos.prc_conceptos.nombre.Contains(busquedaTextbox.Text) ||
                     a.fecha_nota.Day.Equals(busquedaNum) ||
                     a.fecha_nota.Year.Equals(busquedaNum))).ToList().
                     Union(DB.contexto.prc_date_pagos.AsEnumerable().Where(
                     a =>
                         (a.fecha_nota >= dateInicio.SelectedDate && a.fecha_nota <= dateFin.SelectedDate) &&
                         a.fecha_nota.ToString("MMMM").Contains(busquedaTextbox.Text))).ToList();
                }
                //miResultado = DB.contexto.prc_view_date_pagos.ToList();


                //var query = (from view in miResultado
                //             join find in resultadoConsulta on view.fecha_nota+view.Expr1+view.nombre equals find.fecha_nota+find.prc_status.nombre+find.prc_pagos.prc_conceptos.nombre
                //             select view).ToList();

                //miResultadoReport = (query as IList);
                return resultadoConsulta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void busquedaTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            gridPagosProgramados.ItemsSource = busquedaAvanzada();
        }

        private void dateInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateInicio.DisplayDateEnd = dateFin.SelectedDate;
            dateFin.DisplayDateStart = dateInicio.SelectedDate;
            if (dateInicio.SelectedDate != null && dateFin.SelectedDate != null)
            {
                gridPagosProgramados.ItemsSource = busquedaAvanzada();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FormReporter form = new FormReporter(miResultadoReport);
            form.ShowDialog();
            gridPagosProgramados.ItemsSource = busquedaAvanzada();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            WindowProgramarPagos vtnNuevoPago = new WindowProgramarPagos();
            vtnNuevoPago.ShowDialog();
            gridPagosProgramados.ItemsSource = busquedaAvanzada();
        }
        private bool isManualEditCommit;
        private void gridPagosProgramados_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
                        gridPagosProgramados.ItemsSource = busquedaAvanzada();
                    }
                    else
                    {
                        grid.CancelEdit();
                    }
                    isManualEditCommit = false;
                }
            }
        }

        private void chbxEdicion_Checked(object sender, RoutedEventArgs e)
        {
            gridPagosProgramados.IsReadOnly = false;
            MessageBox.Show("Edición habilitada.", "Editar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void chbxEdicion_Unchecked(object sender, RoutedEventArgs e)
        {
            gridPagosProgramados.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            busquedaTextbox.Focus();
        }

        private void btnCancelarPago_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cancelar servicio");
        }
    }
}
