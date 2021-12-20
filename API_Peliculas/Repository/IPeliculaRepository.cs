using API_Peliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Repository
{
    public interface IPeliculaRepository
    {
        ICollection<Pelicula> GetPeliculas();

        Pelicula GetPelicula(int id);

        bool ExistsPelicula(string name);

        bool ExistsPelicula(int id);

        bool CreatePelicula(Pelicula pelicula);

        bool UpdatePelicula(Pelicula pelicula);

        bool DeletePelicula(int id);

        bool Save();
    }
}
