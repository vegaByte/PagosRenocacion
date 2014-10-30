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
using PagosRenovacion.Commands;

namespace PagosRenovacion
{
    /// <summary>
    /// Lógica de interacción para WindowProgramarPagos.xaml
    /// </summary>
    public partial class WindowProgramarPagos : Window
    {
        Validator validator;
        prc_pagos pago;
        prc_date_pagos datePagoObj;
        //prc_date_pagos_semanales datePagoSemanalObj;
        enum tipoCalendar { UNICO,SEMANAL,QUINCENAL,MENSUAL,BIMESTRAL,TRIMESTRAL,CUATRIMESTRAL,SEMESTRAL,ANUAL };
        int tipoCalendarInt;
        DateTime time;

        public WindowProgramarPagos()
        {
            InitializeComponent();
            validator = new Validator();
            tipoCalendarInt = 0;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (validator.ValidaCerrar())
                this.Close();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            validator.validaFloat(sender, e);
        }

        private void cmbxTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbxTipoPago.SelectedIndex < 5)
            {
               // Habilitar-Deshabilitar Controles unicos
                datePago.Visibility = System.Windows.Visibility.Visible;
                cmbxDiaSemana.Visibility = System.Windows.Visibility.Hidden;
                cmbxDiaNumero.Visibility = System.Windows.Visibility.Hidden;
                lbRecordarHasta.Visibility = System.Windows.Visibility.Hidden;
                dateRecordarHasta.Visibility = System.Windows.Visibility.Hidden;
                dateRecordarInicio.Visibility = System.Windows.Visibility.Hidden;
                lbFechaInicio.Visibility = System.Windows.Visibility.Hidden;

                tipoCalendarInt = (int)tipoCalendar.UNICO;
            }
            else if (cmbxTipoPago.SelectedIndex == 5)
            {
                // Habilitar-Deshabilitar Controles semana
                datePago.Visibility = System.Windows.Visibility.Hidden;
                cmbxDiaSemana.Visibility = System.Windows.Visibility.Visible;
                cmbxDiaNumero.Visibility = System.Windows.Visibility.Hidden;
                lbRecordarHasta.Visibility = System.Windows.Visibility.Visible;
                dateRecordarHasta.Visibility = System.Windows.Visibility.Visible;
                dateRecordarInicio.Visibility = System.Windows.Visibility.Visible;
                lbFechaInicio.Visibility = System.Windows.Visibility.Visible;

                tipoCalendarInt = (int)tipoCalendar.SEMANAL;
            }
            else
            {
                switch (cmbxTipoPago.SelectedIndex)
                {
                    case 6:
                        tipoCalendarInt = (int)tipoCalendar.QUINCENAL;
                        break;
                    case 7:
                        tipoCalendarInt = (int)tipoCalendar.MENSUAL;
                        break;
                    case 8:
                        tipoCalendarInt = (int)tipoCalendar.BIMESTRAL;
                        break;
                    case 9:
                        tipoCalendarInt = (int)tipoCalendar.TRIMESTRAL;
                        break;
                    case 10:
                        tipoCalendarInt = (int)tipoCalendar.CUATRIMESTRAL;
                        break;
                    case 11:
                        tipoCalendarInt = (int)tipoCalendar.SEMESTRAL;
                        break;
                    case 12:
                        tipoCalendarInt = (int)tipoCalendar.ANUAL;
                        break;
                }
                // Habilitar-Deshabilitar Controles periodos
                datePago.Visibility = System.Windows.Visibility.Hidden;
                cmbxDiaSemana.Visibility = System.Windows.Visibility.Hidden;
                cmbxDiaNumero.Visibility = System.Windows.Visibility.Visible;
                lbRecordarHasta.Visibility = System.Windows.Visibility.Visible;
                dateRecordarHasta.Visibility = System.Windows.Visibility.Visible;
                dateRecordarInicio.Visibility = System.Windows.Visibility.Visible;
                lbFechaInicio.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (validator.ValidaString(txtNombre.Text))
            {
                //if(datePago.Visibility == System.Windows.Visibility.Visible)
                //    if(validator.ValidaDatePickerNoNull(datePago))
                try
                {
                    float val = Convert.ToSingle(txtNombre.Text, CultureInfo.CreateSpecificCulture("en-US"));
                    pago = new prc_pagos
                    {
                        id_pagos = 0,
                        date_inicio = dateRecordarInicio.SelectedDate.Value,
                        date_final = dateRecordarHasta.SelectedDate.Value,
                        fk_id_tipopagos = (int)cmbxTipoPago.SelectedValue,
                        fk_id_conceptos = (int)cmbxConcepto.SelectedValue,
                        fk_id_usuarios = (App.Current.Resources["UsuarioActualR"] as UsuarioActual).UserName,
                        activo = true
                        
                    };
                    DB.contexto.prc_pagos.Add(pago);
                    DB.contexto.SaveChanges();

                    if (cmbxTipoPago.SelectedIndex < 5)
                    {
                        //tipoCalendarInt = (int)tipoCalendar.UNICO;
                        if (datePago.SelectedDate != null)
                        {
                            if (validator.ValidaString(txtNombre.Text))
                            {
                                string cadenaMostrar = "";
                                datePagoObj = new prc_date_pagos
                                {
                                    id_date_pagos = 0,
                                    fk_id_pagos = DB.contexto.prc_pagos.Max(a => a.id_pagos),
                                    fk_id_status = 3,
                                    fecha_nota = datePago.SelectedDate.Value,
                                    monto = val
                                };
                                cadenaMostrar += datePago.SelectedDate.Value.ToString();
                                if (cadenaMostrar.Equals(""))
                                    MessageBox.Show("No se ha guardado ningun pago.\nVerifique el rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                else
                                {
                                    DB.contexto.prc_date_pagos.Add(datePagoObj);
                                    DB.contexto.SaveChanges();
                                    MessageBox.Show("Fecha(s) guardada(s):,\n" + cadenaMostrar, "Guardado con éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    this.Close();
                                }
                            }
                        } else
                            MessageBox.Show("Favor de seleccionar la fecha del pago.", "Fecha no elegida", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (cmbxTipoPago.SelectedIndex == 5)
                    {
                        //tipoCalendarInt = (int)tipoCalendar.SEMANAL;

                      //  Fecha inicial (hoy)
                        time = dateRecordarInicio.SelectedDate.Value;
                        
                        switch (cmbxDiaSemana.SelectedIndex)
                        {
                            case 0:
                                while (time.DayOfWeek != DayOfWeek.Monday)
                                    time = time.AddDays(1);
                                break;
                            case 1:
                                while (time.DayOfWeek != DayOfWeek.Thursday)
                                    time = time.AddDays(1);
                                break;
                            case 2:
                                while (time.DayOfWeek != DayOfWeek.Wednesday)
                                    time = time.AddDays(1);
                                break;
                            case 3:
                                while (time.DayOfWeek != DayOfWeek.Tuesday)
                                    time = time.AddDays(1);
                                break;
                            case 4:
                                while (time.DayOfWeek != DayOfWeek.Friday)
                                    time = time.AddDays(1);
                                break;
                            case 5:
                                while (time.DayOfWeek != DayOfWeek.Saturday)
                                    time = time.AddDays(1);
                                break;
                        }
                        
                        //MessageBox.Show(time.ToString());
                        //agrega una nota por cada 7 dias hasta la fecha indicada como final
                        string cadenaMostrar = "";
                        for (int x = 7; DateTime.Compare(time, dateRecordarHasta.SelectedDate.Value)<=0; time = time.AddDays(x))
                        {
                            datePagoObj = new prc_date_pagos
                            {
                                id_date_pagos = 0,
                                fk_id_pagos = DB.contexto.prc_pagos.Max(a => a.id_pagos),
                                fk_id_status = 3,
                                fecha_nota = time,
                                monto = val
                            };
                            cadenaMostrar += time + "\n";
                            DB.contexto.prc_date_pagos.Add(datePagoObj);
                        }
                        if (cadenaMostrar.Equals(""))
                            MessageBox.Show("No se ha guardado ningun pago.\nVerifique el rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            //DB.contexto.prc_date_pagos.Add(datePagoObj);
                            DB.contexto.SaveChanges(); 
                            MessageBox.Show("Fecha(s) guardada(s):\n" + cadenaMostrar, "Guardado con éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.Close();
                        }
                        //DB.contexto.SaveChanges();
                    }
                    else
                    {
                        switch (cmbxTipoPago.SelectedIndex)
                        {
                            case 6:
                                //tipoCalendarInt = (int)tipoCalendar.QUINCENAL;
                                //Fecha Inicial (hoy)
                                //DateTime time = Convert.ToDateTime(DateTime.Today, new CultureInfo("es-ES"));
                                string cadenaMostrar = "";
                                for (int x = 15; DateTime.Compare(time, dateRecordarHasta.SelectedDate.Value) <= 0; time = time.AddDays(x))
                                {
                                    cadenaMostrar += time.ToString() + "\n";
                                    datePagoObj = new prc_date_pagos
                                    {
                                        id_date_pagos = 0,
                                        fk_id_pagos = DB.contexto.prc_pagos.Max(a => a.id_pagos),
                                        fk_id_status = 3,
                                        fecha_nota = time,
                                        nota = "",
                                        monto = val
                                    };
                                    DB.contexto.prc_date_pagos.Add(datePagoObj);
                                }
                                if (cadenaMostrar.Equals(""))
                                    MessageBox.Show("No se ha guardado ningun pago.\nVerifique el rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                else
                                {
                                    DB.contexto.SaveChanges();
                                    MessageBox.Show("Fecha(s) guardada(s):,\n" + cadenaMostrar, "Guardado con éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    this.Close();
                                }
                                break;
                            case 7:
                                //tipoCalendarInt = (int)tipoCalendar.MENSUAL;
                                agregarPagosPorMeses(1, (int)cmbxDiaNumero.SelectedValue);
                                break;
                            case 8:
                                //tipoCalendarInt = (int)tipoCalendar.BIMESTRAL;
                                agregarPagosPorMeses(2, (int)cmbxDiaNumero.SelectedValue);
                                break;
                            case 9:
                                //tipoCalendarInt = (int)tipoCalendar.TRIMESTRAL;
                                agregarPagosPorMeses(3, (int)cmbxDiaNumero.SelectedValue);
                                break;
                            case 10:
                                //tipoCalendarInt = (int)tipoCalendar.CUATRIMESTRAL;
                                agregarPagosPorMeses(4, (int)cmbxDiaNumero.SelectedValue);
                                break;
                            case 11:
                                //tipoCalendarInt = (int)tipoCalendar.SEMESTRAL;
                                agregarPagosPorMeses(6, (int)cmbxDiaNumero.SelectedValue);
                                break;
                            case 12:
                                //tipoCalendarInt = (int)tipoCalendar.ANUAL;
                                agregarPagosPorMeses(12, (int)cmbxDiaNumero.SelectedValue);
                                break;
                        }
                    }

                    //MessageBox.Show("Guardado con éxito.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido guardar, ha ocurrido un error de tipo:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void agregarPagosPorMeses(int meses, int diaRecordar)
        {
            float val = Convert.ToSingle(txtNombre.Text, CultureInfo.CreateSpecificCulture("en-US"));
            try
            {
                string cadenaMostrar = "";
                //Fecha Inicial (hoy)
                //DateTime time = Convert.ToDateTime(DateTime.Today, new CultureInfo("es-ES"));

                while (time.Day != diaRecordar)
                    time = time.AddDays(1);

                for (int x = meses; DateTime.Compare(time, dateRecordarHasta.SelectedDate.Value) <= 0; time = time.AddMonths(x))
                {
                    cadenaMostrar += time.ToString() + "\n";
                    datePagoObj = new prc_date_pagos
                    {
                        id_date_pagos = 0,
                        fk_id_pagos = DB.contexto.prc_pagos.Max(a => a.id_pagos),
                        fk_id_status = 3,
                        fecha_nota = time,
                        nota = "",
                        monto = val
                    };
                    DB.contexto.prc_date_pagos.Add(datePagoObj);
                }
                if (cadenaMostrar.Equals(""))
                    MessageBox.Show("No se ha guardado ningun pago.\nVerifique el rango de fechas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    DB.contexto.SaveChanges();
                    MessageBox.Show("Fecha(s) guardada(s):,\n" + cadenaMostrar, "Guardado con éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido guardar, ha ocurrido un error de tipo:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dateRecordarHasta.SelectedDate = DateTime.Today;

            cmbxDiaSemana.SelectedIndex = 0;
            cmbxConcepto.SelectedIndex = 0;
            cmbxTipoPago.SelectedIndex = 0;
            cmbxDiaNumero.SelectedIndex = 0;
            cmbxDiaNumero.SelectedIndex = 0;
            cmbxDiaNumero.Visibility = System.Windows.Visibility.Hidden;
            cmbxDiaSemana.Visibility = System.Windows.Visibility.Hidden;
            tipoCalendarInt = 0;
            cmbxDiaNumero = validator.fillListBoxWithRange(cmbxDiaNumero, 1, 31);
            DateTime timetemp = Convert.ToDateTime(DateTime.Today, new CultureInfo("es-ES"));
            dateRecordarHasta.SelectedDate = timetemp.AddMonths(1);
            dateRecordarInicio.SelectedDate = timetemp;
            time = timetemp;
            dateRecordarInicio.DisplayDateEnd = dateRecordarHasta.SelectedDate;
            dateRecordarInicio.DisplayDateStart = timetemp;
            datePago.DisplayDateStart = timetemp;
            dateRecordarHasta.DisplayDateStart = dateRecordarInicio.SelectedDate;

            cmbxConcepto.ItemsSource = DB.contexto.prc_conceptos.ToList();
        }

        private void dateRecordarInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateRecordarInicio.DisplayDateEnd = dateRecordarHasta.SelectedDate;
            dateRecordarHasta.DisplayDateStart = dateRecordarInicio.SelectedDate;
        }
    }
}
