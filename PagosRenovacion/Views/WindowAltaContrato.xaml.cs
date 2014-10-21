using PagosRenovacion.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PagosRenovacion
{
    /// <summary>
    /// Lógica de interacción para WindowAltaContrato.xaml
    /// </summary>
    public partial class WindowAltaContrato : Window
    {
        DateTime timetemp;
        int numeroColumns;
        Button btn;
        StackPanel stackPanel;
        RowDefinition row;
        Label ctrlFecha, nota;
        DatePicker datePicker;
        TextBox textBox;
        Validator validator;

        private object objeto;

        public WindowAltaContrato()
        {
            InitializeComponent();
            numeroColumns = 0;
            validator = new Validator();
            cmbxActividades.SelectedIndex = 0;
            objeto = new object();
        }

        private void Button_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (validator.ValidaCerrar())
                this.Close();            
        }

        private void btnAgregarNota_Click(object sender, RoutedEventArgs e)
        {
            numeroColumns++;
            stackPanel = new StackPanel();
            row = new RowDefinition();
            btn = new Button();
            ctrlFecha = new Label();
            datePicker = new DatePicker();
            nota = new Label();
            textBox = new TextBox();

            miScroll.ScrollToEnd();

            //definir propiedades del boton
            btn.Click += miBoton_Click;
            btn.Height = miBoton.Height;
            btn.Width = miBoton.Width;
            btn.Background = miBoton.Background;
            btn.HorizontalAlignment = miBoton.HorizontalAlignment;
            btn.Template = (this.Resources["ButtonCloseTemplate"] as ControlTemplate);
            btn.Content = miBoton.Content;
            btn.Foreground = miBoton.Foreground;
            btn.FontWeight = miBoton.FontWeight;
            btn.Margin = miBoton.Margin;

            //definir propiedades del boton
            ctrlFecha.Content = lblFecha.Content;
            ctrlFecha.HorizontalAlignment = lblFecha.HorizontalAlignment;
            ctrlFecha.FontSize = lblFecha.FontSize;

            //definir propiedades del boton
            datePicker.Height = datePago.Height;
            datePicker.Width = datePago.Width;
            datePicker.FontSize = datePago.FontSize;
            datePicker.HorizontalAlignment = datePago.HorizontalAlignment;
            datePicker.DisplayDateStart = timetemp;

            //definir propiedades del label nota
            nota.Content = ctrNota.Content;
            nota.Height = ctrNota.Height;
            nota.Width = ctrNota.Width;
            nota.FontSize = ctrNota.FontSize;
            nota.HorizontalAlignment = ctrNota.HorizontalAlignment;

            //definir propiedades del label nota
            textBox.Height = txtNota.Height;
            textBox.Width = txtNota.Width;
            textBox.FontSize = txtNota.FontSize;
            textBox.HorizontalAlignment = txtNota.HorizontalAlignment;

            //Agregar todo el cochinero
            GridControlsCollection.RowDefinitions.Add(row);
            GridControlsCollection.Children.Add(stackPanel);
            Grid.SetRow(stackPanel, numeroColumns);
            stackPanel.Children.Add(btn);
            stackPanel.Children.Add(ctrlFecha);
            stackPanel.Children.Add(datePicker);
            stackPanel.Children.Add(nota);
            stackPanel.Children.Add(textBox);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            prc_contratos contrato;
            prc_date_contratos dateContrato;
            if (validator.ValidaString(txtConcepto.Text))
            {
                var vtnEmergente = MessageBox.Show("Realmente desea agregar el nuevo contrato?",
                        "Nuevo registro", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (vtnEmergente == MessageBoxResult.Yes)
                {
                    try
                    { 
                        contrato = new prc_contratos
                        {
                            id_contratos=0,
                            concepto=validator.RecortaString(txtConcepto.Text),
                            fk_id_usuarios = (App.Current.Resources["UsuarioActualR"] as UsuarioActual).UserName,
                            fk_id_actividades=(int)cmbxActividades.SelectedValue
                        };
                        DB.contexto.prc_contratos.Add(contrato);

                        DB.contexto.SaveChanges();

                        DateTime fecha = new DateTime();
                        string nota = "";
                        // validar los campos de las fechas
                        if (validator.ValidaNotasContrato(GridControlsCollection))
                        {
                            // agregar cada nota del conjunto de stack panels en GridControlsCollection
                            foreach (StackPanel spanel in GridControlsCollection.Children)
                            {
                                foreach (UIElement element in spanel.Children)
                                {
                                    if (element is TextBox)
                                    {
                                        nota = (element as TextBox).Text;
                                    }
                                    if (element is DatePicker)
                                    {
                                        fecha = (element as DatePicker).SelectedDate.Value;
                                    }
                                }
                                // agregar la nueva nota con su fecha correspondiente al stackpanel actual
                                dateContrato = new prc_date_contratos
                                {
                                    id_date_contratos = 0,
                                    fk_id_contratos = DB.contexto.prc_contratos.Max(a => a.id_contratos),
                                    fecha_nota = fecha,
                                    nota = nota
                                };
                                DB.contexto.prc_date_contratos.Add(dateContrato);
                            }

                            DB.contexto.SaveChanges();
                            MessageBox.Show("Contrato guardado con éxito.", "Guardado exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void miBoton_Click(object sender, RoutedEventArgs e)
        {
            GridControlsCollection.Children.Remove((sender as Button).Parent as UIElement);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            //GridControlsCollection.Children.Remove((objeto as Button).Parent as UIElement);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timetemp = Convert.ToDateTime(DateTime.Today, new CultureInfo("es-ES"));
            txtConcepto.Focus();
            datePago.DisplayDateStart = timetemp;
        }

        private void txtConcepto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            e.Handled = rgx.IsMatch(e.Text);
        }
    }
}
