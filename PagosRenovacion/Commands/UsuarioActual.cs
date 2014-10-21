using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.Commands
{
    public class UsuarioActual
    {
        string userName;
        string nombre;
        string nivel;
        string puesto;

        public string Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        public string Puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
