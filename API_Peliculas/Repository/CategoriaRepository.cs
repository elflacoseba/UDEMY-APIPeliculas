using API_Peliculas.Data;
using API_Peliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateCategoria(Categoria categoria)
        {
            _db.Categoria.Add(categoria);
            return Save();
        }

        public bool DeleteCategoria(int id)
        {
            Categoria cat = _db.Categoria.FirstOrDefault(c => c.Id == id);

            if (cat != null)
            {
                _db.Categoria.Remove(cat);
                return Save();
            }
            else
            {
                return false;
            }
            
        }

        public bool ExistsCategoria(string name)
        {
            return _db.Categoria.Any(c => c.Nombre.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool ExistsCategoria(int id)
        {
            return _db.Categoria.Any(c => c.Id == id);
        }

        public Categoria GetCategoria(int id)
        {
            return _db.Categoria.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _db.Categoria.ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            _db.Categoria.Update(categoria);
            return Save();
        }
    }
}
