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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Objects;
using PagosRenovacion;
using System.Windows.Navigation;
using System.Text.RegularExpressions;

namespace PagosRenovacion.Views
{
    /// <summary>
    /// Lógica de interacción para WindowAdministrar.xaml
    /// </summary>
    public partial class WindowAdministrar : Window
    {
        dbpagoscontratosEntities dataEntities = new dbpagoscontratosEntities();
        Validator validator;
        public WindowAdministrar()
        {
            grid = this.dataGrid1;
            columna0 = this.columna;
            InitializeComponent();
            dataGrid1.ItemsSource = DB.contexto.prc_conceptos.ToList();
            columna.Binding = new Binding("id_conceptos");
            cmbxCategoria.SelectedIndex = 0;
            validator = new Validator();
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (validator.ValidaCerrar())
                this.Close();
        }
        public DataGrid grid;
        public DataGridTextColumn columna0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            //dataGrid1.DataContext = DB.contexto.prc_actividades.ToList();
            
            //columna.Binding = new Binding("id_status");
            //dataGrid1.SetBinding( = new Binding("");
            dataGrid1.IsReadOnly = true;
        }

        private void cmbxCategoria_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void cmbxCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualizaCategoria(cmbxCategoria.SelectedIndex);
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

                    var vtnEmergente = MessageBox.Show("Realmente desea editar el registro?", "Cambio en registro", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (vtnEmergente == MessageBoxResult.Yes)
                    {
                        grid.CommitEdit(DataGridEditingUnit.Row, true);
                        DB.contexto.SaveChanges();
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
            dataGrid1.IsReadOnly = false;
            MessageBox.Show("Edición habilitada.", "Editar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void chbxEdicion_Unchecked(object sender, RoutedEventArgs e)
        {
            dataGrid1.IsReadOnly = true;
        }
        
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            prc_actividades newActividad;
            prc_conceptos newConcepto;
            prc_status newStatus;
            prc_tipopagos newTipoPago;

            string nombreCategoria = (cmbxCategoria.SelectedItem as TextBlock).Text;
            string nuevoNombre = txtNombre.Text.ToString();
            if (validator.ValidaString(nuevoNombre))
            {
                //quitar espacios al principio
                nuevoNombre = validator.RecortaString(nuevoNombre);

                var vtnEmergente = MessageBox.Show("Realmente desea agregar \"" + nuevoNombre +
                    "\" a la categoría de \"" + nombreCategoria + "\"?",
                    "Nuevo registro", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (vtnEmergente == MessageBoxResult.Yes)
                {
                    try
                    {

                        switch (cmbxCategoria.SelectedIndex)
                        {
                            case 0:
                                newConcepto = new prc_conceptos
                                {
                                    id_conceptos = 0,
                                    nombre = nuevoNombre
                                };
                                DB.contexto.prc_conceptos.Add(newConcepto);
                                break;
                            case 3:
                                newTipoPago = new prc_tipopagos
                                {
                                    id_tipopagos = 0,
                                    nombre = nuevoNombre
                                };
                                DB.contexto.prc_tipopagos.Add(newTipoPago);
                                break;
                            case 1:
                                newStatus = new prc_status
                                {
                                    id_status = 0,
                                    nombre = nuevoNombre
                                };
                                DB.contexto.prc_status.Add(newStatus);
                                break;
                            case 2:
                                newActividad = new prc_actividades
                                {
                                    id_actividades = 0,
                                    nombre = nuevoNombre
                                };
                                DB.contexto.prc_actividades.Add(newActividad);
                                break;
                        }
                        DB.contexto.SaveChanges();
                        actualizaCategoria(cmbxCategoria.SelectedIndex);
                        MessageBox.Show(nombreCategoria+ " guardado con éxito.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        public void actualizaCategoria(int index)
        {
            switch (index)
            {
                case 0:
                    dataGrid1.ItemsSource = DB.contexto.prc_conceptos.ToList();
                    columna.Binding = new Binding("id_conceptos");
                    break;
                case 3:
                    dataGrid1.ItemsSource = DB.contexto.prc_tipopagos.ToList();
                    columna.Binding = new Binding("id_tipopagos");
                    break;
                case 1:
                    dataGrid1.ItemsSource = DB.contexto.prc_status.ToList();
                    columna.Binding = new Binding("id_status");
                    break;
                case 2:
                    dataGrid1.ItemsSource = DB.contexto.prc_actividades.ToList();
                    columna.Binding = new Binding("id_actividades");
                    break;
            }
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            e.Handled = rgx.IsMatch(e.Text);
        }

    }
}
