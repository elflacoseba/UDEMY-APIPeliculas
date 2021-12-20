using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static API_Peliculas.Models.Pelicula;

namespace API_Peliculas.Models.Dtos
{
    public class PeliculaDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        public string RutaImagen { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria.")]
        public int Duracion { get; set; }

        public TipoClasificacion Clasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int CategoriaID { get; set; }

        public Categoria Categoria { get; set; }
    }
}
