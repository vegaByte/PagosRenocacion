using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.Commands
{
    public class LoginCommand
    {
        string user;
        string pass;
        public LoginCommand(string usuario, string password)
        {
            user = usuario;
            pass = password;
        }
        public bool buscaRegistro()
        {
            try
            {
                var buscarUser = DB.contexto.prc_usuarios.ToList().FirstOrDefault(a => a.id_usuarios == user
                    && a.password == pass);

                if (buscarUser != null)
                {
                    (App.Current.Resources["UsuarioActualR"] as UsuarioActual).Nombre = buscarUser.nombre;
                    (App.Current.Resources["UsuarioActualR"] as UsuarioActual).UserName = buscarUser.id_usuarios;
                    (App.Current.Resources["UsuarioActualR"] as UsuarioActual).Nivel = buscarUser.nivel;
                    (App.Current.Resources["UsuarioActualR"] as UsuarioActual).Puesto = buscarUser.puesto;
                    //UsuarioActual userActual = new UsuarioActual
                    //{
                    //    UserName = buscarUser.id_usuarios,
                    //    Nombre = buscarUser.nombre,
                    //    Nivel = buscarUser.nivel,
                    //    Puesto = buscarUser.puesto
                    //};
                    //MessageBox.Show("Bienvenido "+userActual.Nombre);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexión.","Error",MessageBoxButton.OK,MessageBoxImage.Stop);
                return false;
            }
        }
    }
}
