using API_Peliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Repository
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(int id);

        bool ExistsCategoria(string name);

        bool ExistsCategoria(int id);

        bool CreateCategoria(Categoria categoria);

        bool UpdateCategoria(Categoria categoria);

        bool DeleteCategoria(int id);

        bool Save();
    }
}
