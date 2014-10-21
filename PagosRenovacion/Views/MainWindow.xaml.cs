using PagosRenovacion.Commands;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PagosRenovacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Validator validator;
        public MainWindow()
        {
            InitializeComponent();
            txtuser.Focus();
            validator = new Validator();
        }

        private void btndone_Click(object sender, RoutedEventArgs e)
        {
            if (validator.ValidaString(txtuser.Text) && validator.ValidaString(txtpass.Password))
            {
                LoginCommand login = new LoginCommand(txtuser.Text, txtpass.Password);

                if (login.buscaRegistro())
                {
                    new Window1().Show();
                    this.Close();
                }
                else
                    MessageBox.Show("El usuario y/o contraseña son incorrectos.", "Usuario no existente", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
