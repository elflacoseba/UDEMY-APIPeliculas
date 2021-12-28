using API_Peliculas.Data;
using API_Peliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ExistsUsuario(string name)
        {
            return _db.Usuario.Any(u => u.NombreUsuario.Trim().ToLower() == name.Trim().ToLower());
        }

        public Usuario GetUsuario(int id)
        {
            return _db.Usuario.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuario.OrderBy(u => u.NombreUsuario).ToList();
        }

        public Usuario Login(Usuario usuario, string password)
        {
            throw new NotImplementedException();
        }

        public Usuario Registro(Usuario usuario, string password)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
