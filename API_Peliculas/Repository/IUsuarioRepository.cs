using API_Peliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Repository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();

        Usuario GetUsuario(int id);

        bool ExistsUsuario(string name);

        Usuario Registro(Usuario usuario, string password);

        Usuario Login(string usuario, string password);

        bool Save();
    }
}
