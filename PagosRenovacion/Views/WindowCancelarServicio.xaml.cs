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

namespace PagosRenovacion.Views
{
    /// <summary>
    /// Lógica de interacción para WindowCancelarServicio.xaml
    /// </summary>
    public partial class WindowCancelarServicio : Window
    {
        private prc_pagos myservicio;
        Validator val;
        public WindowCancelarServicio()
        {
            InitializeComponent();
        }
        public WindowCancelarServicio(prc_pagos servicio)
        {
            InitializeComponent();
            val = new Validator();
            myservicio = servicio;

            txtServicio.Text = servicio.prc_conceptos.nombre;
            txtTipoPago.Text = servicio.prc_tipopagos.nombre;
            txtFInicio.Text =servicio.date_inicio.ToString().Substring(0,10);
            txtFTerminacion.Text = servicio.date_final.ToString().Substring(0,10);
        }
        private void refrescaGridPagosEliminar()
        {
            List<prc_date_pagos> pagos = DB.contexto.prc_date_pagos.Where(a => a.fecha_nota > dateCancel.SelectedDate && a.fk_id_pagos == myservicio.id_pagos).ToList();
            gridPagosEliminar.ItemsSource = pagos;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<prc_date_pagos> pagos = DB.contexto.prc_date_pagos.Where(a => a.fk_id_pagos == myservicio.id_pagos && a.fk_id_status==3).ToList();
            pagos.OrderBy(a=>a.fecha_nota);
            prc_date_pagos ultimoPago = pagos.First();
            DateTime ultimaFecha = ultimoPago.fecha_nota;
            //DateTime timetemp = Convert.ToDateTime(DateTime.Today, new CultureInfo("es-ES"));
            //dateCancel.SelectedDate = timetemp;
            dateCancel.DisplayDateStart = ultimaFecha;
            dateCancel.SelectedDate = ultimaFecha;
            refrescaGridPagosEliminar();




        }
        private bool firstOpen = true;
        private void dateCancel_CalendarOpened(object sender, RoutedEventArgs e)
        {
            if(firstOpen)
                MessageBox.Show("ADVERTENCIA: Los pagos con fechas posteriores a la fecha de cancelación seleccionada en esta opción serán eliminadas.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            firstOpen = false;
            dateCancel.IsDropDownOpen = true;
        }

        private void dateCancel_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            refrescaGridPagosEliminar();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (val.ValidaDatePickerNoNull(dateCancel))
            {
                var vtn = MessageBox.Show("Realmente desea cancelar el servicio \""+myservicio.prc_conceptos.nombre+"\" con fecha de cancelación el día \""+dateCancel.SelectedDate.ToString().Substring(0,10)+"\"?\n\nADVERTENCIA: Los pagos programados con fecha posterior a la fecha de cancelación serán eliminados.", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (vtn == MessageBoxResult.Yes)
                {
                    List<prc_date_pagos> pagos = DB.contexto.prc_date_pagos.Where(a => a.fecha_nota > dateCancel.SelectedDate && a.fk_id_pagos == myservicio.id_pagos).ToList();
                    int idPago = 0;
                    foreach(var pago in pagos){
                        prc_date_pagos pagoDel = DB.contexto.prc_date_pagos.Where(a => a.id_date_pagos == pago.id_date_pagos).Single();
                        DB.contexto.prc_date_pagos.Remove(pagoDel);
                        idPago = pago.fk_id_pagos;
                    }
                    prc_pagos servicio = DB.contexto.prc_pagos.First(a => a.id_pagos == idPago);
                    servicio.date_final = dateCancel.SelectedDate.Value;
                    servicio.activo = false;
                    DB.contexto.SaveChanges();
                    MessageBox.Show("Servicio cancelado satisfactoriamente.", "Cancelado con Éxito", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
