using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosRenovacion.ViewModels
{
    public class UsuariosVM : ViewModelBase
    {
        public UsuariosVM()
        {
            try
            {
                if (usuariosList == null)
                {
                    usuariosList = new List<prc_usuarios>(DB.contexto.prc_usuarios.ToList());
                }
            }
            catch { }
        }
        List<PagosRenovacion.prc_usuarios> usuariosList;

        public List<PagosRenovacion.prc_usuarios> UsuariosList
        {
            get { return usuariosList; }
            set { usuariosList = value; OnPropertyChanged(); }
        }

    }
}
