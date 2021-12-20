using API_Peliculas.Data;
using API_Peliculas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Repository
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly ApplicationDbContext _db;

        public PeliculaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreatePelicula(Pelicula pelicula)
        {
            _db.Pelicula.Add(pelicula);
            return Save();
        }

        public bool DeletePelicula(int id)
        {
            Pelicula peli = _db.Pelicula.FirstOrDefault(c => c.Id == id);

            if (peli != null)
            {
                _db.Pelicula.Remove(peli);
                return Save();
            }
            else
            {
                return false;
            }
            
        }

        public bool ExistsPelicula(string name)
        {
            return _db.Pelicula.Any(c => c.Nombre.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool ExistsPelicula(int id)
        {
            return _db.Pelicula.Any(c => c.Id == id);
        }

        public Pelicula GetPelicula(int id)
        {
            return _db.Pelicula.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Pelicula> GetPeliculas()
        {
            return _db.Pelicula.OrderBy(p => p.Nombre).ToList();
        }

        public ICollection<Pelicula> GetPeliculasPorCategoria(int categoriaID)
        {
            return _db.Pelicula.Include(p => p.Categoria).Where(c => c.CategoriaID == categoriaID).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePelicula(Pelicula pelicula)
        {
            _db.Pelicula.Update(pelicula);
            return Save();
        }
    }
}
