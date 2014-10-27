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
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            WindowProgramarPagos vtnNuevoPago = new WindowProgramarPagos();
            vtnNuevoPago.ShowDialog();
            //gridPagosProgramados.ItemsSource = busquedaAvanzada();
        }
    }
}
