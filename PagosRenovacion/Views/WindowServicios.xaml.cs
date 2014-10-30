using System;
using System.Collections;
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

namespace PagosRenovacion.Views
{
    /// <summary>
    /// Lógica de interacción para WindowServicios.xaml
    /// </summary>
    public partial class WindowServicios : Window
    {
        public WindowServicios()
        {
            InitializeComponent();
            gridServiciosProgramados.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0FFFF"));
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            WindowProgramarPagos vtnNuevoPago = new WindowProgramarPagos();
            vtnNuevoPago.ShowDialog();
            gridServiciosProgramados.ItemsSource = busquedaAvanzada();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FormReporter form = new FormReporter(miResultadoReport);
            form.ShowDialog();
            gridServiciosProgramados.ItemsSource = busquedaAvanzada();
        }


        List<prc_view_date_pagos> miResultado;
        IList miResultadoReport;
        private List<prc_pagos> busquedaAvanzada()
        {
            //concepto(nombre) fecha(dia, mes, año) status(nombre)
            DateTime busquedaFecha;
            int busquedaNum;

            DateTime.TryParse(busquedaTextbox.Text, out busquedaFecha);
            int.TryParse(busquedaTextbox.Text, out busquedaNum);
            try
            {
                List<prc_pagos> resultadoConsulta;

                if (dateInicio.SelectedDate == null || dateFin.SelectedDate == null)
                {
                    resultadoConsulta = DB.contexto.prc_pagos.Where
                    (a =>
                     a.prc_conceptos.nombre.Contains(busquedaTextbox.Text) ||
                     a.date_inicio.Day.Equals(busquedaNum) ||
                     a.date_inicio.Year.Equals(busquedaNum)).ToList().
                     Union(DB.contexto.prc_pagos.AsEnumerable().Where(
                     a => a.date_inicio.ToString("MMMM").Contains(busquedaTextbox.Text))).ToList();
                }
                else
                {

                    resultadoConsulta = DB.contexto.prc_pagos.Where
                    (a =>
                     (a.date_inicio >= dateInicio.SelectedDate && a.date_inicio <= dateFin.SelectedDate) &&
                     (a.prc_conceptos.nombre.Contains(busquedaTextbox.Text) ||
                     a.date_inicio.Day.Equals(busquedaNum) ||
                     a.date_inicio.Year.Equals(busquedaNum))).ToList().
                     Union(DB.contexto.prc_pagos.AsEnumerable().Where(
                     a => a.date_inicio.ToString("MMMM").Contains(busquedaTextbox.Text))).ToList();



                    /*
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
                         a.fecha_nota.ToString("MMMM").Contains(busquedaTextbox.Text))).ToList(); */
                }
                //miResultado = DB.contexto.prc_view_date_pagos.ToList();


                //var query = (from view in miResultado
                //             join find in resultadoConsulta on view.fecha_nota + view.Expr1 + view.nombre equals find.fecha_nota + find.prc_status.nombre + find.prc_pagos.prc_conceptos.nombre
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            busquedaTextbox.Focus();
        }

        private void dateInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void busquedaTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            gridServiciosProgramados.ItemsSource = busquedaAvanzada();
        }

        private void chbxEdicion_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chbxEdicion_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void gridServiciosProgramados_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (gridServiciosProgramados.SelectedIndex != -1)
            {
                prc_pagos servicio = gridServiciosProgramados.SelectedItem as prc_pagos;
                WindowCancelarServicio vtnCancelar = new WindowCancelarServicio(servicio);
                vtnCancelar.ShowDialog();
                gridServiciosProgramados.ItemsSource = busquedaAvanzada();
            }
            else
                MessageBox.Show("Seleccione el servicio a cancelar.", "Servicio no seleccionado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
