using PagosRenovacion.Commands;
using PagosRenovacion.Views;
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

namespace PagosRenovacion
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        WindowAdministrar vtnAdministrar;
        WindowProgramarPagos vtnProgramarPagos;
        WindowAltaContrato vtnAltaContrato;
        WindowRegistroContratos vtnReportContrato;
        WindowReportePagos vtnReportPagos;
        WindowServicios vtnServiciosContratados;
        public Window1()
        {
            InitializeComponent();
        }

        private void menuItemPagos_Click(object sender, RoutedEventArgs e)
        {
                
        }

        private void menuItemReportePago_Click(object sender, RoutedEventArgs e)
        {
            if (vtnReportPagos == null)
            {
                vtnReportPagos = new WindowReportePagos();
                vtnReportPagos.ShowDialog();
                vtnReportPagos.Unloaded += (s, a) => { vtnReportPagos = null; };
            }
            else
                vtnReportPagos.Focus();
        }
        private void menuItemReporteContrato_Click(object sender, RoutedEventArgs e)
        {
            if (vtnReportContrato == null)
            {
                vtnReportContrato = new WindowRegistroContratos();
                vtnReportContrato.ShowDialog();
                vtnReportContrato.Unloaded += (s, a) => { vtnReportContrato = null; };
            }
            else
                vtnReportContrato.Focus();
        }

        private void menuItemAdministrar_Click(object sender, RoutedEventArgs e)
        {
            if (vtnAdministrar == null)
            {
                vtnAdministrar = new WindowAdministrar();
                vtnAdministrar.ShowDialog();
                vtnAdministrar.Unloaded += (s, a) => { vtnAdministrar = null; };
            }
            else
                vtnAdministrar.Focus();
        }

        private void menuItemNuevoPago_Click(object sender, RoutedEventArgs e)
        {
            if (vtnProgramarPagos == null)
            {
                vtnProgramarPagos = new WindowProgramarPagos();
                vtnProgramarPagos.ShowDialog();
                vtnProgramarPagos.Unloaded += (s, a) => { vtnProgramarPagos = null; };
            }
            else
                vtnProgramarPagos.Focus();
        }

        private void menuItemNuevoContrato_Click(object sender, RoutedEventArgs e)
        {
            if (vtnAltaContrato == null)
            {
                vtnAltaContrato = new WindowAltaContrato();
                vtnAltaContrato.ShowDialog();
                vtnAltaContrato.Unloaded += (s, a) => { vtnAltaContrato = null; };
            }
            else
                vtnAltaContrato.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void windowPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(App.Current.Resources["UsuarioActualR"] as UsuarioActual).Nivel.Equals("0"))
            {
                menuItemCategorias.IsEnabled = false;
            }
        }

        private void menuItemReporteServicios_Click(object sender, RoutedEventArgs e)
        {
            if (vtnServiciosContratados== null)
            {
                vtnServiciosContratados = new WindowServicios();
                vtnServiciosContratados.ShowDialog();
                vtnServiciosContratados.Unloaded += (s, a) => { vtnServiciosContratados = null; };
            }
            else
                vtnServiciosContratados.Focus();

        }
    }
}
