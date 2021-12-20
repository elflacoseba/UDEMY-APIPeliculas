using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Peliculas.Models
{
    public class Pelicula
    {
        public enum TipoClasificacion
        {
            Siete, Trece, Dieciseis, Dieciocho
        }

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string RutaImagen { get; set; }

        public string Descripcion { get; set; }

        public int Duracion { get; set; }

        public TipoClasificacion Clasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

       [ForeignKey("CategoriaID")]
        public int CategoriaID { get; set; }

        public Categoria Categoria { get; set; }
    }
}
